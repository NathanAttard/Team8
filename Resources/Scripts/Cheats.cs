using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    GameManager myGameManager;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            //Kill Player
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            myGameManager.AddAmmoCheat(30);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //Skip Cinematic
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            myGameManager.KillEnemiesCheat();
        }
    }
}
