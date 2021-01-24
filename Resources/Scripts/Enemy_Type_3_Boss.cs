using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Type_3_Boss : Enemy
{
    Coroutine waitCoroutine;
    Coroutine walkCoroutine;
    Coroutine attackCoroutine;
    Coroutine behaviourCoroutine;

    float attackRange;
    bool isAttacking;
    bool isJumpAttacking;
    bool isMidJump;

    float normAcc;
    float walkSpeed;

    bool playerDetected;

    // Start is called before the first frame update
    protected override void Start()
    {
        health = 600;
        coins = 100;
        aggroRange = 30f;
        hitEffect = Resources.Load<GameObject>("Prefabs/Others/HitBlood");
        base.Start();

        walkSpeed = 8f;
        normAcc = 8f;
        attackRange = 4f;

        isAttacking = false;

        playerDetected = false;
        waitCoroutine = StartCoroutine(WaitForPlayer());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator WaitForPlayer()
    {
        while (true)
        {
            if (isAggroed)
            {
                yield return new WaitForSeconds(0.5f);
                Aggroed();
            }

            //get colliders next to this enemy
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, aggroRange);

            //read each collider
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.tag == "playerObject")
                {
                    yield return new WaitForSeconds(0.5f);
                    Aggroed();
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

    void Aggroed()
    {
        myGameManager.DestroyEnemies(this.gameObject);

        playerDetected = true;
        behaviourCoroutine = StartCoroutine(BossBehaviour());
    }

    IEnumerator BossBehaviour()
    {
        walkCoroutine = StartCoroutine(WalkToPlayer());
        attackCoroutine = StartCoroutine(AttackBehaviour());

        while (true)
        {
            if (isAttacking)
            {
                StopWalk();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator AttackBehaviour()
    {
        while (true)
        {
            float distanceToPlayer = Vector3.Distance(this.transform.position, GameData.PlayerPosition);
            float facingAngleToPlayer = Vector3.Angle(this.transform.forward, GameData.PlayerPosition - this.transform.position);

            if (distanceToPlayer > 15f && distanceToPlayer < 30f)
            {
                StartJumpAttack();

                while (isJumpAttacking == true)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }

            else
            {
                //get colliders next to this enemy
                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, attackRange);

                //read each collider
                foreach (Collider collider in hitColliders)
                {
                    if (collider.gameObject.tag == "playerObject" && facingAngleToPlayer <= 70f)
                    {
                        StartAttack();

                        while (isAttacking == true)
                        {
                            yield return new WaitForSeconds(0.1f);
                        }

                        yield return new WaitForSeconds(0.9f);
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator WalkToPlayer()
    {
        StartWalk();

        while (true)
        {
            if (!isMidJump)
            {
                MoveToPlayer();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    void StartWalk()
    {
        this.navAgent.isStopped = false;
        animator.SetBool("isWalk", true);
    }

    void MoveToPlayer()
    {
        navAgent.SetDestination(GameData.PlayerPosition);
    }

    void StopWalk()
    {
        this.navAgent.isStopped = true;
        animator.SetBool("isWalk", false);
    }

    void StartAttack()
    {
        isAttacking = true;
        int randAtk = UnityEngine.Random.Range(1, 3);

        if(randAtk == 1)
        {
            animator.SetBool("isAttack1", true);
        }

        else if(randAtk == 2)
        {
            animator.SetBool("isAttack2", true);
        }
    }

    void StartJumpAttack()
    {
        StopWalk();
        animator.SetBool("isJumpAttack", true);
        isJumpAttacking = true;
    }

    void JumpAttackJump()
    {
        navAgent.isStopped = false;
        MoveToPlayer();
        navAgent.speed = 25f;
        navAgent.acceleration = 40f;
    }

    void MidJump()
    {
        isMidJump = true;
    }

    void JumpAttackLand()
    {
        navAgent.speed = 0f;
        navAgent.acceleration = 0f;
        navAgent.isStopped = true;
    }

    void OnHitJumpAttack()
    {
        AttackHit(140f, 3.5f, 40);
    }

    void OnJumpAttackEnd()
    {
        animator.SetBool("isJumpAttack", false);
        isJumpAttacking = false;
        isMidJump = false;

        navAgent.speed = walkSpeed;
        navAgent.acceleration = normAcc;

        StartWalk();
    }

    void OnHitAttack1()
    {
        AttackHit(70f, 3.5f, 15);
    }

    void OnHitAttack2()
    {
        AttackHit(60f, 5f, 20);
    }

    void OnAttack1End()
    {
        animator.SetBool("isAttack1", false);
        isAttacking = false;

        StartWalk();
    }

    void OnAttack2End()
    {
        animator.SetBool("isAttack2", false);
        isAttacking = false;

        StartWalk();
    }

    protected override void Died()
    {
        try
        {
            StopCoroutine(waitCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        try
        {
            StopCoroutine(walkCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        try
        {
            StopCoroutine(attackCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        try
        {
            StopCoroutine(behaviourCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        this.navAgent.isStopped = true;

        animator.SetBool("isAttack1", false);
        animator.SetBool("isAttack2", false);
        animator.SetBool("isJumpAttack", false);
        animator.SetBool("isWalk", false);

        base.Died();
    }
}
