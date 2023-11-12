using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemy : MonoBehaviour
{
    NavMeshAgent agent;

    GameObject target;

    [SerializeField] LayerMask groundLayer, targetLayer;
    
    //patrol
    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float walkRange;

    //attacking
    float timeBetweenAttacks;
    float attaackTimer;
    bool canAttack;

    //state change
    [SerializeField]float sightRange, attackRange;
    bool targetInSight, targetInAttack;

    void Start()
    {
        target = GameObject.FindWithTag("Tower");
        //Debug.Log(target.GetInstanceID());
        agent = GetComponent<NavMeshAgent>();

    }
    private void Update()
    {
        targetInSight = Physics.CheckSphere(transform.position, sightRange, targetLayer);
        targetInAttack = Physics.CheckSphere(transform.position, attackRange, targetLayer);

        if(!targetInSight && !targetInAttack)
        {
            Debug.Log("Patrol");
            Patrol();
        }
        if(targetInSight && !targetInAttack)
        {
            Debug.Log("Chase");
            Chase();
        }
        if(targetInSight && targetInAttack)
        {
            Debug.Log("Attack");
            Attack();
        }
        /*if(Vector3.Distance(transform.position, destPoint) < 1)
        {
            walkPointSet = false;
        }*/
    }
    void Attack()
    {
        //will do for now
        if (agent.SetDestination(target.transform.position))
        {
            Debug.Log("Target found: " + target.name);
        }
    }
    void Patrol()
    {
        if(!walkPointSet)
        {
            SearchForWalkPoint();
            
        }
        if (walkPointSet)
        {
            agent.SetDestination(destPoint);
        }
        if(Vector3.Distance(transform.position, destPoint)<10) { walkPointSet = false; }
    }
    void SearchForWalkPoint()
    {
        float Z = Random.Range(-walkRange, walkRange);
        float X = Random.Range(-walkRange, walkRange);

        destPoint = new Vector3(transform.position.x + X, transform.position.y, transform.position.z + Z);

        if(Physics.Raycast(destPoint, Vector3.down, 5f, groundLayer))
        {
            walkPointSet = true;
        }
    }
    void Chase()
    {
        if(agent.SetDestination(target.transform.position))
        {
            Debug.Log("Target found: " + target.name);
        }
    }
}
