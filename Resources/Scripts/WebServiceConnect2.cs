using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebServiceConnect2 : MonoBehaviour
{
    private GameManager gamegen;

    Text username;
    Text assaulthead;
    Text handheadshots;

    //The url needed to connect with pythonanywhere
    public string url = "http://westerngraveyardteam8.pythonanywhere.com/";

    private void Start()
    {
        
    }

    public void GetUser(Person person)
    {
        StartCoroutine(GetPersonScore(person));
    }

    IEnumerator GetPersonScore(Person person)
    {
        string jsonPerson = JsonUtility.ToJson(person);

        WWWForm form = new WWWForm();

        //Setup the parameters
        form.AddField("JsonData", jsonPerson);

        string customUrl = url + "getdata";

        //Connect with the service
        WWW w = new WWW(customUrl, form);
        yield return w;

        //If there is an error, print it
        if (!string.IsNullOrEmpty(w.error))
        {
            print(w.error);
        }
        else
        {
            string returnedText = w.text;
            Person objPerson = JsonUtility.FromJson<Person>(returnedText);
            print(objPerson.Name);
            print(objPerson.AssaultHeadshots);
            print(objPerson.HandHeadshots);
            username = GameObject.Find("usernameText").GetComponent<Text>();
            username.text = objPerson.Name;
            assaulthead = GameObject.Find("assaultheadText").GetComponent<Text>();
            assaulthead.text = objPerson.AssaultHeadshots + " Assault Headshots";
            handheadshots = GameObject.Find("handheadshotsText").GetComponent<Text>();
            handheadshots.text = objPerson.HandHeadshots + " Hand Headshots";
        }        
    }
}
