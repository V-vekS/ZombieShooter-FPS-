using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform target;

    [SerializeField] float targetRange = 5f;
    [SerializeField] float lookSpeed = 5f;

    NavMeshAgent navMeshAgent;
    bool isProvoked = false;
    EnemyHealth health;

    float distanceToTarget = Mathf.Infinity;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
       health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
    }

  
    void Update()   
    {
        if (health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
            
         else if (distanceToTarget <= targetRange)
        {
            isProvoked = true;
           
        }
        
    }

    public void OnTakingDamage()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();

        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTheTarget();
        }
        else if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTheTarget();
        }
    }

    private void ChaseTheTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTheTarget()
    {
        GetComponent<Animator>().SetBool("Attack",true);
        

    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);

    }
    void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,targetRange);
    }
}
