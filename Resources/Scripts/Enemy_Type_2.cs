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

    // Start is called before the first frame update
    protected override void Start()
    {
        health = 40;
        damage = 10;
        coins = 25;
        aggroRange = 7f;
        base.Start();

        navAgent = GetComponent<NavMeshAgent>();
        patrolCoroutine = StartCoroutine(PatrolWaypoints(waypoints));        
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
        }
    }


    //Facing the player to attack.. research? idk right now.

    //Create a empty gameobject and make a radius from it that if player goes out of it, they will return to patrol.
}
