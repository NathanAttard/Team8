using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Type_1 : Enemy
{
    Coroutine waitCoroutine;

    // Start is called before the first frame update
    protected override void Start()
    {
        health = 10;
        damage = 5;
        coins = 5;
        aggroRange = 7f;
        base.Start();

        navAgent = GetComponent<NavMeshAgent>();
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

                    StartCoroutine(AttackPlayer());
                    break;
                }
                yield return new WaitForSeconds(0.2f);
            }

            if (playerDetected)
            {
                break;
            }
        }

        yield return null;
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
            navAgent.SetDestination(GameData.PlayerPosition);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
