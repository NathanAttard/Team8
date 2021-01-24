using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonBehaviour : MonoBehaviour
{
    GameManager gameManager;

    InputField InputUsername;

    [SerializeField]
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        this.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                InputUsername = GameObject.Find("UsernameIF").GetComponent<InputField>();
                string username = InputUsername.text;
                GameData.Username = username;
                print(GameData.Username);

                gameManager.changeScene(sceneName);
            });
    }

    // Update is called once per frame
    void Update()
    {
    }
}
