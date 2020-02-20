using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepyAIElevator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float stoppingRange = 5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool idle = false;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Invoke("LeaveElevator", 30f);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget > stoppingRange && !idle){
            navMeshAgent.SetDestination(target.position);
        }   
        else{
            idle = true;
            anim.SetBool("Idle", true);
            navMeshAgent.ResetPath();
            Invoke("TurnAround", 1f);
        }
    }

    public void TurnAround(){
        anim.SetTrigger("TurnAround");
    }

    public void LeaveElevator(){
        anim.SetTrigger("LeaveElevator");
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
}