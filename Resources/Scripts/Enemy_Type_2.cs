using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Type_2 : Enemy
{
    [Tooltip("The waypoints the enemy will move to.")]
    [SerializeField]
    List<Transform> waypoints;

    Coroutine patrolCoroutine;
    Coroutine waitCoroutine;

    float aggroAngle;

    // Start is called before the first frame update
    protected override void Start()
    {
        health = 40;
        damage = 10;
        coins = 25;
        aggroRange = 10f;
        base.Start();

        aggroAngle = 70f;

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
        bool playerDetected = false;
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
                    Debug.Log("Found Player");
                    playerDetected = true;

                    StopCoroutine(patrolCoroutine);
                    StartCoroutine(AttackPlayer());
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

    IEnumerator AttackPlayer()
    {
        //Pause his patrol
        this.navAgent.isStopped = true;

        while (true)
        {
            Debug.Log("Starting Attack");
            //Attack Player
            yield return new WaitForSeconds(0.2f);
        }
    }
}
