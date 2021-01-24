using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public AudioClip swallow;
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

    private void OnTriggerStay(Collider col)
    {

        if(col.gameObject.tag == "playerObject")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioSource.PlayClipAtPoint(swallow, transform.position);

                myGameManager.ChangePlayerHealth(healthGives);

                Destroy(this.gameObject);
            }
        }

        
    }
}
