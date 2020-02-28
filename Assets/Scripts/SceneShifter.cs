using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneShifter : MonoBehaviour
{

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){

        if(other.name == "HeadCollider"){
            anim.SetTrigger("FadeOut");
            Invoke("SwitchScene", 1f);
        }
    }

    private void SwitchScene() {
        SceneManager.LoadScene("Platform");
        anim.SetTrigger("FadeIn");
    }
}
