using System.Collections;
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
        assaultRifle = this.gameObject.transform.GetChild(0).gameObject;
        handGun = this.gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.GetComponent<FpsControllerLPFP>().arms = assaultRifle.transform;
            assaultRifle.SetActive(true);
            handGun.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            this.GetComponent<FpsControllerLPFP>().arms = handGun.transform;
            assaultRifle.SetActive(false);
            handGun.SetActive(true);
        }
    }
}
