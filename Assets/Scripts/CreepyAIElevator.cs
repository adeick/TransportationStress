using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepyAIElevator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float stoppingRange = 5f;
    [SerializeField] float tedWalkDelay = 20f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool pursuit = false;
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
        if(distanceToTarget > stoppingRange && pursuit){
            navMeshAgent.SetDestination(target.position);
        }   
        else if(pursuit){
            pursuit = false;
            anim.SetTrigger("Idle");
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

    public void Pursue(){
        pursuit = true;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
}