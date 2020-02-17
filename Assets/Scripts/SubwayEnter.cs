using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayEnter : MonoBehaviour
{

    [SerializeField] Vector3 EndingPosition; // The 3D coordinates of where you want the train to stop
    [SerializeField] float BrakingDistance = 4f; //How far away from the EndingPosition the train should start to slow down
    [SerializeField] float enterDelay = 120f; // How many seconds until the train enters the subway
    [SerializeField] float speed = 20f; // How fast the train initially enters the subway
    [SerializeField] float BrakeFactor = 0.8f; // How quickly the train brakes once reaching the Braking Distance. 
                                               // 1 = no brake, 0 = immediate stop
    private bool isEntering = false;
    private bool isStopping = false;
    private Vector3 BrakePoint;

/*
The train enters at a set time (EnterDelay) and at a set speed (speed). It will start to slow down at BreakingDistance units 
away from the Ending Position. It will slow down faster or slower based on BrakeFactor.

The train will stop under one of two conditions
 1) It reaches Ending Position
 2) Speed decreases past 0.001 units/sec

 If it reaches EndingPosition at too high speed, it will instantly stop and will not appear realistic.
 If the Speed decreases too quickly, the train will stop before reaching the destination.
*/

    void Start()
    {
        Invoke("StartSubway", enterDelay);
        BrakePoint = new Vector3(0, 0, BrakingDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if(isEntering){
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, EndingPosition + BrakePoint, step);
            if(EndingPosition + BrakePoint == transform.position){
             isEntering = false;
             isStopping = true;
            }
         }
         else if(isStopping){
             speed *= BrakeFactor;
             float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, EndingPosition, step);
            if(EndingPosition == transform.position || speed < 0.001){
             isStopping = false;
            }
         }
    }
    public void StartSubway(){
        isEntering = true;
    }
}
