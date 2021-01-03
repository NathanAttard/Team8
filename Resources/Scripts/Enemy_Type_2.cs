using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy_Type_2 : Enemy
{
    [Tooltip("The waypoints the enemy will move to.")]
    [SerializeField] List<Transform> waypoints;

    [Tooltip("The arrow prefab the enemy will shoot when attacking.")]
    [SerializeField] GameObject arrowPrefab;

    [Tooltip("The speed of the arrow that is shot on attacking")]
    [SerializeField] float arrowForce;

    Coroutine patrolCoroutine;
    Coroutine waitCoroutine;
    Coroutine lookCoroutine;
    Coroutine spotCoroutine;

    GameObject arrowSpawnPoint;

    float aggroAngle;

    
    // Start is called before the first frame update
    protected override void Start()
    {
        health = 40;
        coins = 25;
        aggroRange = 10f;
        hitEffect = Resources.Load<GameObject>("Prefabs/Others/HitBones");
        base.Start();

        aggroAngle = 70f;
        arrowSpawnPoint = this.transform.Find("ArrowSpawnPoint").gameObject;


        patrolCoroutine = StartCoroutine(PatrolWaypoints(waypoints));
        waitCoroutine = StartCoroutine(WaitForPlayer());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator PatrolWaypoints(List<Transform> waypoints)
    {
        Vector3 targetWaypoint;
        Vector3 currentPos;

        animator.SetBool("isWalk", true);

        while (true)
        {
            foreach (Transform currentWayPoint in waypoints)
            {
                //Agent starts moving to the current waypoint
                navAgent.SetDestination(currentWayPoint.position);

                targetWaypoint = new Vector3(currentWayPoint.position.x, 0, currentWayPoint.position.z);

                while (true)
                {
                    currentPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);

                    //If agent is 0.5f within the waypoint, then start moving to the next waypoint.
                    if (Vector3.Distance(targetWaypoint, currentPos) < 0.5f)
                    {
                        break;
                    }

                    yield return new WaitForSeconds(0.3f);
                }
            }

            waypoints.Reverse();
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator WaitForPlayer()
    {
        float facingAngleToPlayer;

        while (true)
        {
            //Get at which angle the player is from the enemy's current facing direction
            facingAngleToPlayer = Vector3.Angle(this.transform.forward, GameData.PlayerPosition - this.transform.position);

            //get colliders next to this enemy
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, aggroRange);

            //read each collider
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.tag == "playerObject" && facingAngleToPlayer < aggroAngle)
                {
                    animator.SetBool("isWalk", false);
                    this.navAgent.isStopped = true;

                    spotCoroutine = StartCoroutine(PlayerSpotted());
                    StopCoroutine(patrolCoroutine);
                    StopCoroutine(waitCoroutine);
                    break;
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator PlayerSpotted()
    {
        lookCoroutine = StartCoroutine(LookAtPlayer());
        yield return new WaitForSeconds(1f);

        while (true)
        {
            AttackPlayer();
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator LookAtPlayer()
    {
        while (true)
        {
            Vector3 playerPos = new Vector3(GameData.PlayerPosition.x, this.gameObject.transform.position.y, GameData.PlayerPosition.z);
            this.transform.LookAt(playerPos);

            yield return new WaitForSeconds(0.2f);
        }
    }

    void AttackPlayer()
    {
        animator.SetBool("isAttack",true);
    }

    void OnAttackEnd()
    {
        animator.SetBool("isAttack", false);
    }

    void SpawnArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.transform.position, arrowSpawnPoint.transform.rotation);
        arrow.transform.Rotate(90f, 0, 0);

        arrow.GetComponent<Rigidbody>().velocity = arrowSpawnPoint.transform.forward * arrowForce;
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
            StopCoroutine(patrolCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        try
        {
            StopCoroutine(lookCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        try
        {
            StopCoroutine(spotCoroutine);
        }
        catch (NullReferenceException)
        {

        }

        this.navAgent.isStopped = true;

        animator.SetBool("isAttack", false);
        animator.SetBool("isWalk", false);        

        base.Died();
    }
}
