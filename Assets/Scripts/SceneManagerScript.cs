using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject josh;
    public Animator joshAnimator;
    [SerializeField] private float joshWalkDelay = 2f;
    [SerializeField] private float joshDestroyDelay = 10f;
    [SerializeField] private GameObject louise;
    public Animator louiseAnimator;
    [SerializeField] private float louiseWalkDelay = 3f;
    [SerializeField] private float louiseDestroyDelay = 10f;
    [SerializeField] private GameObject kate;
    public Animator kateAnimator;
    [SerializeField] private float kateWalkDelay = 3f;
    [SerializeField] private float kateDestroyDelay = 10f;
    [SerializeField] private GameObject david;
    public Animator davidAnimator;
    [SerializeField] private float davidWalkDelay = 3f;
    [SerializeField] private float davidDestroyDelay = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("JoshLeaveElevator", joshWalkDelay);
        Invoke("LouiseLeaveElevator", louiseWalkDelay);
        Invoke("KateLeaveElevator", kateWalkDelay);
        Invoke("DavidLeaveElevator", davidWalkDelay);
        Destroy(josh, joshDestroyDelay);
        Destroy(louise, louiseDestroyDelay);
        Destroy(kate, kateDestroyDelay);
        Destroy(david, davidDestroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoshLeaveElevator()
    {
       joshAnimator.SetTrigger("LeaveElevator");
    }

    public void LouiseLeaveElevator()
    {
        louiseAnimator.SetTrigger("LeaveElevator");
    }
    public void KateLeaveElevator()
    {
        kateAnimator.SetTrigger("LeaveElevator");
    }
    public void DavidLeaveElevator()
    {
        davidAnimator.SetTrigger("LeaveElevator");
    }
}