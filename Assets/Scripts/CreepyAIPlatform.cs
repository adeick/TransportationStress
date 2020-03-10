using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepyAIPlatform : MonoBehaviour
{
    private Transform target;
    [SerializeField] Transform exitTarget;
    [SerializeField] float stoppingRange = 1.2f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool pursuing = true;
    


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.updateRotation = true;
        target = GameObject.Find("VRCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //distanceToTarget = Vector3.Distance(target.position, transform.position);
        //Calculate distance without including y - value.
        distanceToTarget = (float) Math.Sqrt(Math.Pow(target.position.x - transform.position.x, 2f) + Math.Pow(target.position.z - transform.position.z, 2f));
        if(distanceToTarget > stoppingRange){
            navMeshAgent.SetDestination(target.position);
        }
        else{
            navMeshAgent.ResetPath();
            anim.updateRotation = true;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
}