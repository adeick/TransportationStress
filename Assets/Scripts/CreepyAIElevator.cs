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
    [SerializeField] float lookBackDelay1 = 10f; //delay after ted turns around
    [SerializeField] float lookBackDelay2 = 20f; 
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool pursuit = false;
    


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.applyRootMotion = false;
        Invoke("LeaveElevator1", tedLeaveDelay);
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
            Invoke("GlanceBack1", lookBackDelay1);
            Invoke("GlanceBack2", lookBackDelay2);
        }
    }

    // public void TurnAround(){
    //     anim.SetTrigger("TurnAround");
    // }

    public void GlanceBack1(){
        anim.SetBool("LookBehindLeft", target.transform.localPosition.x < 0.4);
        anim.SetTrigger("LookBehind");
    }

     public void GlanceBack2(){
        anim.SetBool("LookBehindLeft", target.transform.localPosition.x < 0.4);
        anim.SetTrigger("LookBehind");
    }

    public void LeaveElevator1(){
        //anim.applyRootMotion = true; //shouldn't change anything 
        anim.SetTrigger("LeaveElevator");
        Invoke("LeaveElevator2", 1.5f);
    }
    public void LeaveElevator2(){
        anim.applyRootMotion = false;
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