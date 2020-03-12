using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepyAIPlatform : MonoBehaviour
{
    private Transform target;
    [SerializeField] float stoppingRange = 1.2f;
    [SerializeField] float resumeRange = 3f;
    [SerializeField] float turningSpeed = 0.01f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    private Animator anim;
    private bool pursuing = true;
    private bool isTurning = false;


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
         
         // get a "forward vector" for each rotation
            var forwardA = rotation * Vector3.forward;
            var forwardB = transform.rotation * Vector3.forward;
        // get a numeric angle for each vector, on the X-Z plane (relative to world forward)
            var angleA = Mathf.Atan2(forwardA.x, forwardA.z);
            var angleB = Mathf.Atan2(forwardB.x, forwardB.z);

        var angleDiff = Mathf.DeltaAngle( angleA, angleB );
        angleDiff *= 57.2958f; //radians to degrees

        if(angleDiff < 0){
            if(!isTurning){
                anim.SetBool("TurnLeftOrRight", true);
            }
        }
        else{
            if(!isTurning){
                anim.SetBool("TurnLeftOrRight", false);
            }
        }
        if(angleDiff < 120 && angleDiff > -120){
            anim.SetFloat("TedTurn", Mathf.Clamp(angleDiff, -30f, 30f));
            anim.ResetTrigger("TurnAround");
        }
        else{
            anim.SetFloat("TedTurn", 0);
            anim.SetTrigger("TurnAround");
        }   
        Debug.Log(angleDiff);  
        if(angleDiff < 15 && angleDiff > -15){
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turningSpeed);  
        }
// old code (below)
        //  float turnFactor = Quaternion.Angle( new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), new Quaternion(0,rotation.y, 0, rotation.w));

        //  if(Quaternion.Slerp(transform.rotation, rotation, turningSpeed).y - transform.rotation.y < 0){
        //      turnFactor *= -1;
        //      if(!isTurning){
        //         anim.SetBool("TurnLeftOrRight", false);
        //     }
        //  }
        //  else{
        //     if(!isTurning){
        //         anim.SetBool("TurnLeftOrRight", true);
        //     }
        //  }
        // if(turnFactor < 120 && turnFactor > -120){
        //     anim.SetFloat("TedTurn", Mathf.Clamp(turnFactor, -30f, 30f));
        //     anim.ResetTrigger("TurnAround");
        // }
        // else{
        //     anim.SetFloat("TedTurn", 0);
        //     anim.SetTrigger("TurnAround");
        // }                    
        // //anim.SetFloat("TedTurn", Mathf.Clamp(turnFactor, -30f, 30f));

        // //anim.SetFloat("TedTurn", 0);
        // Debug.Log(turnFactor);  
        // if(turnFactor < 15 && turnFactor > -15){
        //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, turningSpeed);  
        // }

    }
    void StartTurn(){
        isTurning = true;
        //Debug.Log("Turn started.");
    }
    void EndTurn(){
        isTurning = false;
        //Debug.Log("Turn ended.");
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stoppingRange);
    }
}