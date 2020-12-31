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
            //Get the name of the gameObject that got hit
            string colliderName = col.GetContact(0).thisCollider.name;

            Debug.Log(colliderName);

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
            Died();
        }
    }

    protected virtual void Died()
    {
        myGameManager.AddCoins(coins);
        animator.SetTrigger("Died");
    }

    protected virtual void AfterDeath()
    {
        Destroy(this.gameObject);
    }

    protected virtual void AttackHit(float attackAngle, float attackRange, float pushForce)
    {
        float facingAngleToPlayer = Vector3.Angle(this.transform.forward, GameData.PlayerPosition - this.transform.position);
        bool isPlayerInRange = false;

        //get colliders next to this enemy
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, attackRange);

        //read each collider
        foreach (Collider collider in hitColliders)
        {
            if (collider.gameObject.tag == "playerObject")
            {
                isPlayerInRange = true;
            }
        }

        if(facingAngleToPlayer <= attackAngle && isPlayerInRange)
        {
            Debug.Log("Player Got Hit");
        }
    }
}