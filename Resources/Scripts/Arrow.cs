using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int damage = 10;
    float destroyAfter = 6f;

    Vector3 prevLoc;

    GameManager myGameManager;
    Coroutine rotateCoroutine;

    private void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        StartCoroutine(DestroyAfter());
        rotateCoroutine = StartCoroutine(RotateArrow());
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "playerObject")
        {
            myGameManager.ChangePlayerHealth(-damage);

            Destroy(this.gameObject);
        }

        else if (other.gameObject.tag == "enemyObject" || other.gameObject.tag == "other")
        {

        }

        else
        {
            StopCoroutine(rotateCoroutine);
            this.gameObject.transform.position = prevLoc;

            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(destroyAfter);
        Destroy(this.gameObject);
    }

    IEnumerator RotateArrow()
    {

        while (true)
        {
            //Get current location and wait for the object to move
            prevLoc = this.transform.position;
            yield return new WaitForSeconds(0.02f);

            //Make object look at the previously recorded location
            this.transform.LookAt(prevLoc);
            //Rotate it -90f due to its default rotation
            this.transform.Rotate(-90f, 0, 0);

        }
    }
}
