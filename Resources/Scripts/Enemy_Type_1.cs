using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Type_1 : Enemy
{
    Coroutine waitCoroutine;
    Coroutine walkCoroutine;
    float attackRange;

    // Start is called before the first frame update
    protected override void Start()
    {
        health = 10;
        damage = 5;
        coins = 5;
        aggroRange = 7f;
        base.Start();

        attackRange = 1.8f;

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
            //get colliders next to this enemy
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, aggroRange);

            //read each collider
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.tag == "playerObject")
                {
                    Debug.Log("Found Player");
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
                    this.navAgent.isStopped = true;

                    animator.SetBool("isWalk", false);
                    animator.SetBool("Attack", true);

                    break;
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    protected override void Died()
    {
        base.Died();

        this.navAgent.isStopped = true;
        StopCoroutine(WalkToPlayer());

        animator.SetBool("Attack", false);
        animator.SetBool("isWalk", false);
    }

    void StartWalk()
    {
        this.navAgent.isStopped = false;
        animator.SetBool("isWalk", true);
    }

    void OnHitAttack()
    {
        AttackHit(70f, attackRange, 10f);
    }

    void OnAttackEnd()
    {
        animator.SetBool("Attack", false);
        StartWalk();
    }

}