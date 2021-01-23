using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    GameObject canvasObject;
    

    Collider[] hitColliders;

    bool hitWithTable;

    // Start is called before the first frame update
    void Start()
    {
        canvasObject = GameObject.Find("Canvas/ConversationPanel");
        
        disableCanvas();
    }

    // Update is called once per frame
    void Update()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 3f);

        hitWithTable = false;

        //We are looping in the array hitColliders
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.tag == "Table")
            {
                hitWithTable = true; //The OnTriggerEnter / Exit method can be used instead of this aswell.

            }
        }

        if (hitWithTable == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Table was hit");
                enableCanvas();
            }
        }
        else
        {
            disableCanvas();
        }
    }

    void disableCanvas()
    {
        canvasObject.SetActive(false);
    }

    void enableCanvas()
    {
        canvasObject.SetActive(true);
    }
}
