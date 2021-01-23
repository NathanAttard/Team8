using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    Animator myAnime;
    GameManager myGameManager;
    bool isNextTo;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        myAnime = gameObject.GetComponent<Animator>();
        isNextTo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNextTo)
        {
            if (Input.GetKeyDown(KeyCode.F) && !myAnime.GetBool("isOpen"))
            {
                myAnime.SetBool("isOpen", true);
            }
            else if (Input.GetKeyDown(KeyCode.F) && myAnime.GetBool("isOpen"))
            {
                myAnime.SetBool("isOpen", false);
            }
        }
        
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "playerObject" && !isNextTo)
        {
            myGameManager.DoorInteract(true);
            isNextTo = true;
        }

    }

    private void OnTriggerExit(Collider col)
    {

        if (col.gameObject.tag == "playerObject")
        {
            myGameManager.DoorInteract(false);
            isNextTo = false;
        }

    }
}
