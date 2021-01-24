using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButtonBehaviour : MonoBehaviour
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
                string username = GameData.Username;
                int assaultHeadshot = GameData.AssaultHeadshotsNum;
                int handHeadshots = GameData.HandHeadshotsNum;
                print(GameData.AssaultHeadshotsNum);
                print(GameData.HandHeadshotsNum);

                Person objPerson = new Person();
                objPerson.Name = username;
                objPerson.AssaultHeadshots = assaultHeadshot;
                objPerson.HandHeadshots = handHeadshots;

                GetComponent<WebServiceConnect>().AddUser(objPerson);

                gameManager.changeScene(sceneName);
            });
    }

    // Update is called once per frame
    void Update()
    {
    }
}
