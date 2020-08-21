using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Math;


public class SubwayEnter : MonoBehaviour
{

    float finalXPosition = 15.54f; // The x coordinate of where you want the train to stop
    float offsetXPosition = 10f; //How far away from the final position the train should start to slow down
    float enterDelay = 120f; // How many seconds until the train enters the subway
    float enterSpeed = 20f; // How fast the train initially enters the subway
    float minimumSpeed = 2f; // The minimum speed the train can go after slowing down.
    
    [SerializeField] float BrakeFactor = 0.8f; // How quickly the braking engages from enterSpeed to minimumSpeed. 
                                               // 1 = no brake, 0 = immediate stop
    private bool isEntering = false;
    private bool isStopping = false;
    private Vector3 BrakePoint;
    private Vector3 EndingPosition;

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
        BrakePoint = new Vector3(offsetXPosition, 0, 0);
        EndingPosition = new Vector3(finalXPosition, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(isEntering){
            float step =  enterSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, EndingPosition + BrakePoint, step);
            if(EndingPosition + BrakePoint == transform.position){
             isEntering = false;
             isStopping = true;
            }
         }
         else if(isStopping){
             enterSpeed *= BrakeFactor;
             float step =  System.Math.Max(enterSpeed, minimumSpeed) * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, EndingPosition, step);
            if(EndingPosition == transform.position){
             isStopping = false;
            }
         }
    }
    public void StartSubway(){
        isEntering = true;
    }
}
