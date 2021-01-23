﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class GameManager : MonoBehaviour
{

    GameManager myGameManager;
    int startingPlayerHealth = 100;

    Image healthBar;
    Text coinsText;
    Text ammoText;
    Text doorText;

    Image assaultImg;
    Image handImg;

    // Start is called before the first frame update
    void Start()
    {
        //Using try and catch to give no errors when text is not found in scene
        try
        {
            doorText = GameObject.Find("doorInteract").GetComponent<Text>();
            healthBar = GameObject.Find("healthSlider").GetComponent<Image>();
            coinsText = GameObject.Find("coinsText").GetComponent<Text>();
            ammoText = GameObject.Find("rifleText").GetComponent<Text>();
            assaultImg = GameObject.Find("rifleImg").GetComponent<Image>();
            handImg = GameObject.Find("pistolImg").GetComponent<Image>();
            UpdateUI();

            DoorInteract(false);
        }
        catch(NullReferenceException)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        if(sceneName == "Level_01")
        {
            ResetGameDataValues();
            Debug.Log("Coins: " + GameData.Coins);
            Debug.Log("Health: " + GameData.PlayerHealth);
        }
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //EditorApplication.Exit(0);
        Application.Quit();
    }

    public void AddCoins(int coinsToAdd)
    {
        GameData.Coins += coinsToAdd;
        Debug.Log("Coins: " + GameData.Coins);
        UpdateUI();
    }

    public void UpdateUI()
    {
        //Update In-Game UI
        healthBar.fillAmount = GameData.PlayerHealth / 100f;

        //A condition to execute the method to change the colour of the health bar according to the health of the user
        if (healthBar.fillAmount <= 0.3f)
        {
            SetHealthBarColor(Color.red);
        }
        else if (healthBar.fillAmount <= 0.5f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }

        coinsText.text = GameData.Coins.ToString();

        UIAmmoUpdate();
    }

    //A method to obtain the colour of the health bar 
    public void SetHealthBarColor(Color healthColor)
    {
        healthBar.color = healthColor;
    }

    public void ResetGameDataValues()
    {
        GameData.Coins = 100;
        GameData.PlayerHealth = startingPlayerHealth;
    }

    public void ChangePlayerHealth(int amountToChange)
    {
        Debug.Log("Health Before: " + GameData.PlayerHealth);
        GameData.PlayerHealth += amountToChange;
        Debug.Log("Health After: " + GameData.PlayerHealth);
        UpdateUI();
        CheckRageMode();
    }

    //Functionality 10
    public void CheckRageMode()
    {
        if(GameData.PlayerHealth <= (startingPlayerHealth * 0.3))
        {
            //Enable Rage Mode
        }
    }

    public void AddAmmoCheat(int ammoToAdd)
    {
        GameData.AssaultAmmo += ammoToAdd;
        GameData.HandAmmo += ammoToAdd;

        UpdateUI();
    }

    public void KillEnemiesCheat()
    {
        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("enemyFullGameObject");

        foreach(GameObject currentEnemy in EnemyList)
        {
            currentEnemy.GetComponent<Enemy>().KillCheat();
        }

        UpdateUI();
    }

    public void UIAmmoUpdate()
    {
        if (GameData.IsAssault)
        {
            assaultImg.enabled = true;
            handImg.enabled = false;
            ammoText.text = GameData.AssaultMag + " / " + GameData.AssaultAmmo;
        }
        else if (!GameData.IsAssault)
        {
            assaultImg.enabled = false;
            handImg.enabled = true;
            ammoText.text = GameData.HandMag + " / " + GameData.HandAmmo;
        }
    }

    public void DoorInteract(bool isNextTo)
    {
        doorText.enabled = isNextTo;
    }
}