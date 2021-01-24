using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    static BackgroundMusic myMusicPlayer = null;

    // Start is called before the first frame update
    void Awake()
    {
        //if myMusicPlayer already exists
        if (myMusicPlayer != null)
        {
            //Destroy the new object immediately
            Destroy(this.gameObject);
        }
        else //if myMusicPlayer is null
        {
            //myMusicPlayer is this object and it is no longer null
            myMusicPlayer = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
