using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        this.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                gameManager.changeScene(sceneName);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
