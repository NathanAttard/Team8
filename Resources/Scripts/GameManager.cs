using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    GameObject quitButton;

    // Start is called before the first frame update
    void Start()
    {
        quitButton = GameObject.Find("QuitBtn");
    }

    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        quitButton.GetComponent<Button>().onClick.AddListener(
            () =>
            {
                UnityEditor.EditorApplication.isPlaying = false;
                //EditorApplication.Exit(0);
                Application.Quit();
            });
    }
}
