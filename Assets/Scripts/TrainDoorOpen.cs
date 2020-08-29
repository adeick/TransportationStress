using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDoorOpen : MonoBehaviour
{
        
    [SerializeField] float openDelay = 25f;
    [SerializeField] bool mirror = false;
    [SerializeField] float speed1 = .25f;
    [SerializeField] float speed2 = .4f;

    [SerializeField] float openZ1Offset = -0.2f;
    [SerializeField] float openZ2Offset = -0.1f;

    [SerializeField] float rotationAngle = 12f;
    [SerializeField] float rotationSpeed = 0.3f;// 0 is never and 1 is immediate

//Offset 1 represents the change from the resting position where the doors come out in the Z direction
//Offset 2 represents the change from the RESTING position where the doors come out in the X direction
                                        //not Offset1

    [SerializeField] float openX1Offset = -0.066515f;
    [SerializeField] float openX2Offset = .7f;

    bool doorsOpening1 = false;
    bool doorsOpening2 = false;

    Vector3 IntermediatePosition;
    Vector3 FinalPosition;

    // Start is called before the first frame update
    void Start()
    {
        if(mirror){
            openX1Offset *= -1;
            openX2Offset *= -1;
            rotationAngle *= -1;
        }
        Invoke("OpenTrainDoors", openDelay);
    }

    // Update is called once per frame
    void Update()
    {
         if(doorsOpening1){
            float step =  speed1 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, IntermediatePosition, step);
            //rotate 30degrees?!
            Quaternion newRotation = Quaternion.AngleAxis(rotationAngle, Vector3.up);
            transform.rotation= Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed);
    //
            if(IntermediatePosition == transform.position){
             doorsOpening1 = false;
             doorsOpening2 = true;
            }
         }
         else if(doorsOpening2){
            float step =  speed2 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, FinalPosition, step);
            //rotate back 30 degrees
            Quaternion newRotation = Quaternion.AngleAxis(0, Vector3.up);
            transform.rotation= Quaternion.Slerp(transform.rotation, newRotation, 0.1f);

            if(FinalPosition == transform.position){
             doorsOpening2 = false;
            }
         }
    }

    void OpenTrainDoors(){
        IntermediatePosition = new Vector3(transform.position.x + openX1Offset, transform.position.y, transform.position.z + openZ1Offset);
        FinalPosition = new Vector3(transform.position.x + openX2Offset, transform.position.y, transform.position.z + openZ2Offset);
        doorsOpening1 = true;
    }
}
