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

    [SerializeField] float turningSpeed = 5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool pursuing = true;
    


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        navMeshAgent.updateRotation = true;
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
            anim.SetBool("pursuit", true);
        }
        else{
            navMeshAgent.ResetPath();
            anim.SetBool("pursuit", false);
            FaceTarget(target.position);
            //transform.rotation.SetLookRotation(target.position - transform.position); 
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotationIncrement, Time.time * turningSpeed);
        }
    }
    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        //Debug.Log(Quaternion.Angle(transform.rotation, rotation));
        //Debug.Log(rotation);
        //Debug.Log(transform.rotation);
        float turnFactor = ((transform.rotation.y - rotation.y) * 100);
        anim.SetFloat("TedTurn", 0);
        //anim.SetFloat("TedTurn", turnFactor);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turningSpeed);  
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
}