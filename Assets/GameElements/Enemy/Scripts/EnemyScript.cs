using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    public Transform target;
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 targetWaypoint;

    //[SerializeField] Transform target = GameObject.FindGameObjectsWithTag("Player");
    public float lookRadius;
    public float reactionRadius;
    public float attackRadius;
    // Start is called before the first frame update
    void Start()
    {

        enemyAgent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(enemyAgent.transform.position, target.position);
        if (distance < reactionRadius && distance > attackRadius) 
        {
            enemyAgent.speed = 2f;
            enemyAgent.SetDestination(target.position);
            //Debug.Log(enemyAgent.remainingDistance);
        }
        else if (distance < attackRadius)
        {
            enemyAgent.speed = 0f;
            //Debug.Log(distance);
        }
        if (Vector3.Distance(transform.position, targetWaypoint) < 1 && distance > lookRadius)
        {
            UpdateWaypointIndex();
            UpdateDestination();
            //Debug.Log(targetWaypoint);
        }
        //Debug.Log(Vector3.Distance(transform.position, targetWaypoint));
        //Debug.Log(enemyAgent.destination);
       /* else
        {
            //stay
            enemyAgent.speed= 0f;
            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("isAttacking", false);
        }*/
           
    }

    void UpdateDestination()
    {
        targetWaypoint = waypoints[waypointIndex].position;
        enemyAgent.SetDestination(targetWaypoint);
    }

    void UpdateWaypointIndex()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }

}
