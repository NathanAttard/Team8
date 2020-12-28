using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    GameManager myGameManager;
    int healthGives;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        healthGives = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "playerObject")
        {
            print("Within Health Range");
        }

    }

    private void OnTriggerStay(Collider col)
    {

        if(col.gameObject.tag == "playerObject")
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                myGameManager.ChangePlayerHealth(healthGives);
                Destroy(this.gameObject);
            }
        }
        
    }

    private void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "playerObject")
        {
            print("Out of Health Range");
        }

    }
}
