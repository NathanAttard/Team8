using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy_Type_1 : Enemy
{
    Coroutine waitCoroutine;
    Coroutine walkCoroutine;
    float attackRange;
    bool isAttacking;

    // Start is called before the first frame update
    protected override void Start()
    {
        health = 10;
        damage = 5;
        coins = 5;
        aggroRange = 7f;
        hitEffect = Resources.Load<GameObject>("Prefabs/Others/HitBones");
        base.Start();

        attackRange = 1.8f;
        isAttacking = false;

        waitCoroutine = StartCoroutine(WaitForPlayer());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator WaitForPlayer()
    {
        bool playerDetected = false;

        while (true)
        {
            float facingAngleToPlayer = Vector3.Angle(this.transform.forward, GameData.PlayerPosition - this.transform.position);

            //get colliders next to this enemy
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, aggroRange);

            //read each collider
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.tag == "playerObject" && facingAngleToPlayer <= 70f)
                {
                    playerDetected = true;

                    walkCoroutine = StartCoroutine(WalkToPlayer());
                    break;
                }
            }

            if (playerDetected)
            {
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    IEnumerator WalkToPlayer()
    {
        StartWalk();

        while (true)
        {
            navAgent.SetDestination(GameData.PlayerPosition);

            //get colliders next to this enemy
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, attackRange);

            //read each collider
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.tag == "playerObject")
                {
                    StartAttack();
                    isAttacking = true;

                    while (isAttacking == true)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    void StartWalk()
    {
        this.navAgent.isStopped = false;
        animator.SetBool("isWalk", true);
    }

    void StartAttack()
    {
        this.navAgent.isStopped = true;

        animator.SetBool("isWalk", false);
        animator.SetBool("Attack", true);
    }

    void OnHitAttack()
    {
        AttackHit(70f, attackRange, damage);
    }

    void OnAttackEnd()
    {
        animator.SetBool("Attack", false);
        isAttacking = false;

        StartWalk();
    }
  

    protected override void Died()
    {
        try
        {
            StopCoroutine(walkCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        try
        {
            StopCoroutine(waitCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        this.navAgent.isStopped = true;

        animator.SetBool("Attack", false);
        animator.SetBool("isWalk", false);

        base.Died();        
    }

    
}