using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    GameManager myGameManager;
    GameObject cineIntro;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        cineIntro = GameObject.Find("CineIntro");
        player = GameObject.Find("Player_Prefab");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            myGameManager.ChangePlayerHealth(-70);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            myGameManager.AddAmmoCheat(30);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            cineIntro.SetActive(false);
            player.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            myGameManager.KillEnemiesCheat();
        }
    }
}
