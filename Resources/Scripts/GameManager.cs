using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class GameManager : MonoBehaviour
{
    

    int startingPlayerHealth = 100;

    Image healthBar;
    Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        //Using try and catch to give no errors when text is not found in scene
        try
        {
            healthBar = GameObject.Find("healthSlider").GetComponent<Image>();
            coinsText = GameObject.Find("coinsText").GetComponent<Text>();
            UpdateUI();
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
}