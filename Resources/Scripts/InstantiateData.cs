using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Person objPerson = new Person();
        print(objPerson);

        GetComponent<WebServiceConnect2>().GetUser(objPerson);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
