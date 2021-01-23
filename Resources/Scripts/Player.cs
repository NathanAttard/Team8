using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

public class Player : MonoBehaviour
{
    GameObject assaultRifle;
    GameObject handGun;

    GameManager myGameManager;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();

        GameData.PlayerObject = this.gameObject;

        GameData.IsAssault = true;

        assaultRifle = this.gameObject.transform.GetChild(0).gameObject;
        handGun = this.gameObject.transform.GetChild(1).gameObject;

        assaultRifle.SetActive(true);
        handGun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameData.PlayerPosition = this.transform.position;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.GetComponent<FpsControllerLPFP>().arms = assaultRifle.transform;
            assaultRifle.SetActive(true);
            handGun.SetActive(false);

            GameData.IsAssault = true;
            myGameManager.UIAmmoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.GetComponent<FpsControllerLPFP>().arms = handGun.transform;
            assaultRifle.SetActive(false);
            handGun.SetActive(true);

            GameData.IsAssault = false;
            myGameManager.UIAmmoUpdate();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            myGameManager.ToggleObjectives();
        }
    }
}
