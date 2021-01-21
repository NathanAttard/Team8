using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebServiceConnect : MonoBehaviour
{
    private GameManager gamegen;

    //The url needed to connect with pythonanywhere
    public string url = "http://westerngraveyardteam8.pythonanywhere.com/";

    public void AddUser(Person person)
    {
        StartCoroutine(PassDataPerson(person));
    }

    IEnumerator PassDataPerson(Person person)
    {
        string jsonPerson = JsonUtility.ToJson(person);
        print(jsonPerson);

        //Setup the form
        WWWForm form = new WWWForm();

        //Setup the parameters
        //jsonPerson filled up with string data
        form.AddField("JsonData", jsonPerson);

        //Setup the URL
        string customUrl = url + "passdata";

        //Connect with the service
        WWW w = new WWW(customUrl, form);
        yield return w;

        //If there is an error, print it
        if(!string.IsNullOrEmpty(w.error))
        {
            print(w.error);
        }
        else
        {
            string returnedText = w.text;
            print(returnedText);
        }
    }
}
