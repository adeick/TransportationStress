using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepyAIElevator : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform exitTarget;
    [SerializeField] float stoppingRange = 5f;
    [SerializeField] float tedWalkDelay = 20f;
    [SerializeField] float tedLeaveDelay = 60f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool pursuit = false;
    


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Invoke("LeaveElevator", tedLeaveDelay);
        Invoke("Pursue", tedWalkDelay);
    }

    // Update is called once per frame
    void Update()
    {
        //distanceToTarget = Vector3.Distance(target.position, transform.position);
        distanceToTarget = (float)Math.Sqrt(Math.Pow(target.position.x-transform.position.x, 2f) + Math.Pow(target.position.z-transform.position.z, 2f));
        if(distanceToTarget > stoppingRange && pursuit){
            navMeshAgent.SetDestination(target.position);
        }   
        else if(pursuit){
            pursuit = false;
            navMeshAgent.ResetPath();
            anim.applyRootMotion = true;
            anim.SetTrigger("TurnAround");
        }
    }

    // public void TurnAround(){
    //     anim.SetTrigger("TurnAround");
    // }

    public void LeaveElevator(){
        anim.applyRootMotion = false;
        anim.SetTrigger("LeaveElevator");
        target = exitTarget;
        pursuit = true;
    }

    public void Pursue(){
        pursuit = true;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
}