using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneShifter : MonoBehaviour
{

    public Animator anim;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.activeSceneChanged += ChangedActiveScene;
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
        
        player.transform.RotateAround(transform.position, transform.up, 180f);
        player.transform.position = new Vector3(12.5f,-0.745f,-4.186f); // position for platform
        SceneManager.LoadScene("Platform");
        anim.SetTrigger("FadeIn");
    }

    // private void ChangedActiveScene(Scene current, Scene next){
    //     if(next.name == "Platform" || next.name == "Train"){
    //         anim.SetTrigger("FadeOut");
    //     }
    // }
}
