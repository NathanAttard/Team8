using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    [Tooltip("The current active conversation")]
    public ConversationScriptableObject CurrentConversation;

    [Tooltip("Option 1 Text")]
    public TMP_Text Option1;

    [Tooltip("Option 2 Text")]
    public TMP_Text Option2;

    [Tooltip("Option 3 Text")]
    public TMP_Text Option3;

    [Tooltip("Option 4 Text")]
    public TMP_Text Option4;

    [Tooltip("Not Selected Option Colour")]
    public Color DefaultOptionColor;

    [Tooltip("Selected Option Colour")]
    public Color SelectedOptionColor;

    //conversation menu index
    private int indexConversation;

    GameObject noCoinsObject;

    // Start is called before the first frame update
    void Start()
    {
        noCoinsObject = GameObject.Find("Canvas/noCoinsPanel");

        ResetIndexConversation();
        RenderConversation();

        disableNoCoins();
    }

    // Update is called once per frame
    void Update()
    {
        //Check for mouse input
        float mouseScrollDirection = Input.GetAxis("Mouse ScrollWheel");

        if(mouseScrollDirection < 0f)
        {
            indexConversation += 1;
        }
        else if(mouseScrollDirection > 0f)
        {
            indexConversation -= 1;
        }

        if(indexConversation >= CurrentConversation.MenuConversation.Count)
        {
            indexConversation = CurrentConversation.MenuConversation.Count - 1;
        }

        if(indexConversation < 0)
        {
            indexConversation = 0;
        }

        RenderConversation();
    }

    void disableNoCoins()
    {
        noCoinsObject.SetActive(false);
    }

    IEnumerator enableNoCoins()
    {
        noCoinsObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        noCoinsObject.SetActive(false);
    }

    private void RenderConversation()
    {
        Option1.gameObject.SetActive(false);
        Option2.gameObject.SetActive(false);
        Option3.gameObject.SetActive(false);
        Option4.gameObject.SetActive(false);

        Option1.color = DefaultOptionColor;
        Option2.color = DefaultOptionColor;
        Option3.color = DefaultOptionColor;
        Option4.color = DefaultOptionColor;

        for (int i=0; i<CurrentConversation.MenuConversation.Count; i++)
        {
            if (i == 0)
            {
                //FirstOption
                Option1.gameObject.SetActive(true);
                Option1.text = CurrentConversation.MenuConversation[0].Title;
                if (indexConversation == 0)
                {
                    Option1.color = SelectedOptionColor;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        print("1");
                        if(GameData.Coins >= 50)
                        {
                            GameData.Coins -= 50;
                            GameData.AssaultAmmo += 80;
                        }
                        else
                        {
                            StartCoroutine(enableNoCoins());
                        }
                    }
                }
                
            }
            else if (i == 2)
            {
                //SecondOption
                Option2.gameObject.SetActive(true);
                Option2.text = CurrentConversation.MenuConversation[1].Title;
                if (indexConversation == 1)
                {
                    Option2.color = SelectedOptionColor;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        print("2");
                        if (GameData.Coins >= 50)
                        {
                            GameData.Coins -= 50;
                            GameData.HandAmmo += 40;
                        }
                        else
                        {
                            StartCoroutine(enableNoCoins());
                        }
                    }
                }
                
            }
            else if (i == 3)
            {
                //ThirdOption
                Option3.gameObject.SetActive(true);
                Option3.text = CurrentConversation.MenuConversation[2].Title;
                if (indexConversation == 2)
                {
                    Option3.color = SelectedOptionColor;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        print("3");
                        if (GameData.Coins >= 100)
                        {
                            GameData.Coins -= 100;
                            GameData.AssaultAmmo += 200;
                        }
                        else
                        {
                            StartCoroutine(enableNoCoins());
                        }
                    }
                }
            }
            else
            {
                //FourthOption
                Option4.gameObject.SetActive(true);
                Option4.text = CurrentConversation.MenuConversation[3].Title;
                if (indexConversation == 3)
                {
                    Option4.color = SelectedOptionColor;
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        print("4");
                        if (GameData.Coins >= 100)
                        {
                            GameData.Coins -= 100;
                            GameData.HandAmmo += 100;
                        }
                        else
                        {
                            StartCoroutine(enableNoCoins());
                        }
                    }
                }
            }
        }
    }

    private void ResetIndexConversation()
    {
        indexConversation = 0;
    }
}
