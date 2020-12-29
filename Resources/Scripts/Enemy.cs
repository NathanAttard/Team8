using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected int coins;

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

        if (col.gameObject.tag == "playerHandBullet" || col.gameObject.tag == "playerAssaultBullet")
        {
            //Get the name of the gameObject that got hit
            string colliderName = col.GetContact(0).thisCollider.name;

            if(colliderName == "Enemy_Head")
            {
                if(col.gameObject.tag == "playerHandBullet")
                {
                    health -= (GameData.HandBulletDMG * GameData.HeadShotMulti);
                    GameData.HandHeadshotsNum += 1;
                    Debug.Log("Hand Headshots: " + GameData.HandHeadshotsNum);
                }
                
                else if(col.gameObject.tag == "playerAssaultBullet")
                {
                    health -= (GameData.AssaultBulletDMG * GameData.HeadShotMulti);
                    GameData.AssaultHeadshotsNum += 1;
                    Debug.Log("Assault Headshots: " + GameData.AssaultHeadshotsNum);
                }
                
            }

            else if (colliderName == "Enemy_Body")
            {
                if (col.gameObject.tag == "playerHandBullet")
                {
                    health -= GameData.HandBulletDMG;
                    Debug.Log("Hit by Hand");
                }

                else if (col.gameObject.tag == "playerAssaultBullet")
                {
                    health -= GameData.AssaultBulletDMG;
                    Debug.Log("Hit by Assault");
                }
                
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