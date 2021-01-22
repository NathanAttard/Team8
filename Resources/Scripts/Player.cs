﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FPSControllerLPFP;

public class Player : MonoBehaviour
{
    GameObject assaultRifle;
    GameObject handGun;

    // Start is called before the first frame update
    void Start()
    {
        GameData.PlayerObject = this.gameObject;

        assaultRifle = this.gameObject.transform.GetChild(0).gameObject;
        handGun = this.gameObject.transform.GetChild(1).gameObject;
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
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.GetComponent<FpsControllerLPFP>().arms = handGun.transform;
            assaultRifle.SetActive(false);
            handGun.SetActive(true);
        }
    }
}
