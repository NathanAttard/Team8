using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hitbox : MonoBehaviour
{
    GameObject parentGameObject;

    void Start()
    {
        parentGameObject = this.transform.parent.gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        parentGameObject.GetComponent<Enemy>().OnCollisionEnterChild(collision);
    }
}
