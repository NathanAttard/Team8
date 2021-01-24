using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using FPSControllerLPFP;

public class GameManager : MonoBehaviour
{

    GameManager myGameManager;
    int startingPlayerHealth = 100;

    bool isTabPressed = true;
    bool enteredRage;

    Image healthBar;
    Text coinsText;
    Text ammoText;
    Text doorText;
    Text objectiveText1;
    Text objectiveText2;
    Text rageTimer;
    Image assaultImg;
    Image handImg;
    GameObject objectivePanel;
    GameObject ragePanel;

    int rageModeDur = 20;

    Coroutine rageCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        //Using try and catch to give no errors when text is not found in scene
        try
        {
            objectiveText1 = GameObject.Find("objectiveText1").GetComponent<Text>();
            objectiveText2 = GameObject.Find("objectiveText2").GetComponent<Text>();
            doorText = GameObject.Find("doorInteract").GetComponent<Text>();
            healthBar = GameObject.Find("healthSlider").GetComponent<Image>();
            coinsText = GameObject.Find("coinsText").GetComponent<Text>();
            ammoText = GameObject.Find("rifleText").GetComponent<Text>();
            assaultImg = GameObject.Find("rifleImg").GetComponent<Image>();
            handImg = GameObject.Find("pistolImg").GetComponent<Image>();
            rageTimer = GameObject.Find("rageTimer").GetComponent<Text>();
            ragePanel = GameObject.Find("ragePanel").gameObject;
            objectivePanel = GameObject.Find("objectivePanel");
            UpdateUI();

            ToggleObjectives();
            ToggleObjectives();

            DoorInteract(false);
            ragePanel.SetActive(false);
            enteredRage = false;
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
        int prevHealth = GameData.PlayerHealth;
        GameData.PlayerHealth += amountToChange;

        if(prevHealth <= (startingPlayerHealth * 0.3) && GameData.PlayerHealth > (startingPlayerHealth * 0.3))
        {
            enteredRage = false;
        }

        UpdateUI();
        CheckRageMode();
    }

    //For Functionality 10 - Rage Mode
    public void CheckRageMode()
    {
        if(GameData.PlayerHealth <= (startingPlayerHealth * 0.3) && !enteredRage)
        {
            enteredRage = true;
            RageMode();
            ragePanel.SetActive(true);

            GameData.PlayerObject.GetComponent<Player>().isRage = true;

            if(rageCoroutine != null)
            {
                StopCoroutine(rageCoroutine);
            }

            rageCoroutine = StartCoroutine(RageModeInPlay());
        }
    }

    //For Functionality 10 - Rage Mode
    IEnumerator RageModeInPlay()
    {
        for(int i = rageModeDur; i >= 0; i--)
        {
            rageTimer.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        StoppedRageMode();
        GameData.PlayerObject.GetComponent<Player>().isRage = false;
    }

    //Functionality 10 - Rage Mode
    public void RageMode()
    {
        GameData.AssaultBulletDMG = 5;
        GameData.HandBulletDMG = 4;

        GameData.PlayerObject.GetComponent<FpsControllerLPFP>().walkingSpeed = 10f;
        GameData.PlayerObject.GetComponent<FpsControllerLPFP>().runningSpeed = 18f;

        try
        {
            GameData.PlayerObject.GetComponentInChildren<AutomaticGunScriptLPFP>().fireRate = 25f;
        }
        catch (NullReferenceException)
        {

        }
    }

    //For Functionality 10 - Rage Mode
    public void StoppedRageMode()
    {
        ragePanel.SetActive(false);

        GameData.AssaultBulletDMG = 2;
        GameData.HandBulletDMG = 3;

        GameData.PlayerObject.GetComponent<FpsControllerLPFP>().walkingSpeed = 5f;
        GameData.PlayerObject.GetComponent<FpsControllerLPFP>().runningSpeed = 9f;

        try
        {
            GameData.PlayerObject.GetComponentInChildren<AutomaticGunScriptLPFP>().fireRate = 11.5f;
        }
        catch (NullReferenceException)
        {

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

    public void ToggleObjectives()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;

        if(sceneName == "Level_01")
        {
            if (isTabPressed)
            {
                objectiveText2.enabled = false;
                objectiveText1.enabled = true;
                objectivePanel.SetActive(true);
                isTabPressed = false;
            }
            else if (!isTabPressed)
            {
                objectiveText2.enabled = false;
                objectiveText1.enabled = false;
                objectivePanel.SetActive(false);
                isTabPressed = true;
            }
        }
        else if(sceneName == "Level_02")
        {
            if (isTabPressed)
            {
                objectiveText1.enabled = false;
                objectiveText2.enabled = true;
                objectivePanel.SetActive(true);
                isTabPressed = false;
            }
            else if (!isTabPressed)
            {
                objectiveText1.enabled = false;
                objectiveText2.enabled = false;
                objectivePanel.SetActive(false);
                isTabPressed = true;
            }
        }
        
        
    }

    //If too many enemies are aggroed, boss becomes laggy,
    //so when boss is Aggroed, rest of the enemies are destroyed
    public void DestroyEnemies(GameObject enemyStayAlive)
    {
        GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("enemyFullGameObject");

        foreach (GameObject currentEnemy in EnemyList)
        {
            if(currentEnemy != enemyStayAlive)
            {
                currentEnemy.GetComponent<Enemy>().KillCheatWithoutCoins();
                Destroy(currentEnemy);
            }
            
        }
    }
}