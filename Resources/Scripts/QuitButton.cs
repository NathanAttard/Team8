using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    GameManager myGameManager;

    // Start is called before the first frame update
    void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();

        this.GetComponent<Button>().onClick.AddListener(() => {
            myGameManager.QuitGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
