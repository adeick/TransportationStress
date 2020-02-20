using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Vector3 EndingPosition;
    [SerializeField] float delay = 5f;
    [SerializeField] float speed = 5f;
    private bool init = false;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Begin", delay);
    }

    // Update is called once per frame
    void Update()
    {
        if(init){
             float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, EndingPosition, step);
            if(EndingPosition == transform.position){
             init = false;
            }
         }
    }
    public void Begin(){
        init = true;
    }
}
