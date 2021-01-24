using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OutroAnimation : MonoBehaviour
{
    Animator trainAnimator;
    Animator cinemachineAnimator;

    GameObject cinemachine;
    GameObject player;

    PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        trainAnimator = GameObject.Find("Train").GetComponent<Animator>();
        cinemachineAnimator = GameObject.Find("CM vcam1").GetComponent<Animator>();

        cinemachine = GameObject.Find("TrainCinemachine");
        player = GameObject.Find("Player_Prefab");

        director = GameObject.Find("Timeline").GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "playerObject")
        {
            //Debug.Log("player");

            trainAnimator.SetBool("Move", true);
            cinemachineAnimator.SetBool("Play", true);

            cinemachine.SetActive(true);
            player.SetActive(false);

            director.Play();
        }
    }
}
