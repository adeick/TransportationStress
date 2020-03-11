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
    [SerializeField] float resumeRange = 3f;
    [SerializeField] float turningSpeed = 0.01f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool pursuing = true;
    


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("VRCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //distanceToTarget = Vector3.Distance(target.position, transform.position);
        //Calculate distance without including y - value.
        distanceToTarget = (float) Math.Sqrt(Math.Pow(target.position.x - transform.position.x, 2f) + Math.Pow(target.position.z - transform.position.z, 2f));
        if(distanceToTarget > stoppingRange && pursuing){
            anim.applyRootMotion = false;
            navMeshAgent.SetDestination(target.position);
            anim.SetBool("pursuit", true);
        }
        else if(distanceToTarget < resumeRange){
            navMeshAgent.ResetPath();
            anim.SetBool("pursuit", false);
            anim.applyRootMotion = true;
            FaceTarget(target.position);
            pursuing = false;
            //transform.rotation.SetLookRotation(target.position - transform.position); 
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotationIncrement, Time.time * turningSpeed);
        }
        else{
            pursuing = true;
        }
    }
    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        //Debug.Log(Quaternion.Angle( new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0,rotation.y, 0, rotation.w)));

        //                     //Debug.Log(rotation.y + "  " + transform.rotation.y);
         float turnFactor = Quaternion.Angle( new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0,rotation.y, 0, rotation.w));
         if(Quaternion.Slerp(transform.rotation, rotation, turningSpeed).y - transform.rotation.y < 0){
             turnFactor *= -1;
         }
                            
        anim.SetFloat("TedTurn", Mathf.Clamp(turnFactor, -30f, 30f));

        //anim.SetFloat("TedTurn", 0);
        //Debug.Log(Quaternion.Slerp(transform.rotation, rotation, turningSpeed).y - transform.rotation.y);  
        if(turnFactor < 15 && turnFactor > -15){
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turningSpeed);  
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
}