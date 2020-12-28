using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected int coins;
    protected int headshotMulti;

    protected GameManager myGameManager;

    protected virtual void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
    }

    protected virtual void Update()
    {
    }

    protected virtual void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "playerBullet")
        {
            //Get the name of the gameObject that got hit
            string colliderName = col.GetContact(0).thisCollider.name;

            if(colliderName == "Enemy_Head")
            {
                health -= (GameData.BulletDMG * headshotMulti);
                GameData.HeadshotsNum += 1;
                Debug.Log("Headshots: " + GameData.HeadshotsNum);
            }

            else if (colliderName == "Enemy_Body")
            {
                health -= GameData.BulletDMG;
            }

            Destroy(col.gameObject);
            CheckIfDead();
        }
    }

    protected virtual void CheckIfDead()
    {
        if (health <= 0)
        {
            myGameManager.AddCoins(coins);
            Destroy(this.gameObject);
        }
    }
}