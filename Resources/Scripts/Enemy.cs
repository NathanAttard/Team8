using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected int coins;

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    protected virtual void CheckIfDead()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "playerBullet")
        {
            //Get the name of the gameObject that got hit
            string colliderName = col.GetContact(0).thisCollider.name;

            if(colliderName == "Enemy_Head")
            {
                Debug.Log("HEADSHOT");
                health -= 5;
            }

            else if (colliderName == "Enemy_Body")
            {
                Debug.Log("Hit");
                health -= 2;
            }

            Destroy(col.gameObject);
            CheckIfDead();
        }
    }
}