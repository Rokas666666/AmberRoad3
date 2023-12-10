using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class GroundEnemy : MonoBehaviour
{
    private Enemy me;

    NavMeshAgent agent;

    GameObject target;

    [SerializeField] LayerMask groundLayer, targetLayer;

    [SerializeField]
    private float damageRange = 1.0f;

    //patrol
    Vector3 destPoint;
    bool walkPointSet;
    [SerializeField] float walkRange;

    //attacking
    float timeBetweenAttacks;
    float attackTimer;
    bool canAttack;

    //state change
    [SerializeField]float sightRange, attackRange;
    bool targetInSight, targetInAttack;

    //debuffs
    [SerializeField]
    float initialSpeed = 0.5f;
    private float debuffDuration = 5f;

    private float HitDelay = 1.0f;

    void Start()
    {
        me = GetComponent<Enemy>();
        target = GameObject.FindWithTag("Tower");
        SetTargetHPS();
        StartCoroutine(HitTower());
        agent = GetComponent<NavMeshAgent>();
        agent.speed = initialSpeed;
    }
  
    private void Update()
    {
        targetInSight = Physics.CheckSphere(transform.position, sightRange, targetLayer);
        targetInAttack = Physics.CheckSphere(transform.position, attackRange, targetLayer);
        if (target == null)
        {
            target = GameObject.FindWithTag("Tower");
            if (target == null)
            {
                Patrol();
            }
            else
            {
                SetTargetHPS();
                StartCoroutine(HitTower());
            }
        }
        if (!targetInSight && !targetInAttack)
        {
            //Debug.Log("Patrol");
            Patrol();
        }
        if(targetInSight && !targetInAttack)
        {
            //Debug.Log("Chase");
            Chase();
        }
        if(targetInSight && targetInAttack)
        {
            //Debug.Log("Attack");
            Attack();
        }
        /*if (Vector3.Distance(transform.position, destPoint) < 1)
        {
            walkPointSet = false;
        }*/
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, attackRange);
    }
    void Attack()
    {
        //will do for now
        if (agent.SetDestination(target.transform.position))
        {
            //Debug.Log("Attack target found: " + target.name);
        }
        //transform.LookAt(target.transform);
       // agent.isStopped = true;
        if(!canAttack)
        {
            canAttack = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        canAttack = false;
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
        Vector3 distanceToDestPoint = transform.position - destPoint;
        if(distanceToDestPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
        //if(Vector3.Distance(transform.position, destPoint)<10) { walkPointSet = false; }
    }
    void SearchForWalkPoint()
    {
        float Z = Random.Range(-walkRange, walkRange);
        float X = Random.Range(-walkRange, walkRange);

        destPoint = new Vector3(transform.position.x + X, transform.position.y, transform.position.z + Z);

        if(Physics.Raycast(destPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }
    void Chase()
    {
        if(agent.SetDestination(target.transform.position))
        {
            //Debug.Log("Chase target found: " + target.name);
        }

    }
    public void ApplySpeedDebuff()
    {
        if(agent != null)
        {
            agent.speed = initialSpeed * 0.5f;
            StartCoroutine(RemoveDebuff());
        }
    }
    public IEnumerator RemoveDebuff()
    {
        yield return new WaitForSeconds(debuffDuration);
        if (agent != null)
        {
            agent.speed = initialSpeed;
        }
    }
    private void SetTargetHPS()
    {
        me.SetTargetHPSystem(target.GetComponent<HealthManager>());
    }
    private void DoDamage()
    {
        float dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
        Debug.Log($"Distance to target {dist}");
        if (dist <= damageRange)
        {
            me.DamageBuilding();
        }
    }
    private IEnumerator HitTower()
    {
        yield return new WaitForSeconds(HitDelay);
        while (true)
        {
            if (target == null)
                break;
            DoDamage();
            yield return new WaitForSeconds(HitDelay);
        }
        yield return null;
    }
}
