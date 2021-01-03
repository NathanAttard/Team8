using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected int health;
    protected int damage;
    protected int coins;
    protected float aggroRange;

    protected NavMeshAgent navAgent;
    protected GameManager myGameManager;
    protected Animator animator;

    protected GameObject hitEffect;

    protected virtual void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
    }

    public virtual void OnCollisionEnterChild(Collision col)
    {
        if (col.gameObject.tag == "playerHandBullet" || col.gameObject.tag == "playerAssaultBullet")
        {
            Instantiate(hitEffect, col.gameObject.transform.position, Quaternion.identity);
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
                }

                else if (col.gameObject.tag == "playerAssaultBullet")
                {
                    health -= GameData.AssaultBulletDMG;
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
            Died();
        }
    }

    protected virtual void Died()
    {
        myGameManager.AddCoins(coins);

        Destroy(this.transform.Find("Enemy_Head").gameObject);
        Destroy(this.transform.Find("Enemy_Body").gameObject);

        animator.SetTrigger("Died");
    }

    protected virtual void AfterDeath()
    {
        Destroy(this.gameObject);
    }

    protected virtual void AttackHit(float attackAngle, float attackRange, int attackDamage)
    {
        float facingAngleToPlayer = Vector3.Angle(this.transform.forward, GameData.PlayerPosition - this.transform.position);

        //get colliders next to this enemy
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, attackRange);

        //read each collider
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.tag == "playerObject")
            {
                if (facingAngleToPlayer <= attackAngle)
                {
                    myGameManager.ChangePlayerHealth(-attackDamage);
                }
            }
        }        
    }
}