using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class EnableNavMeshObstacle : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshObstacle obstacle;

    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
    }
//obstacle will be enabled in the platform scene and train scene
    void ChangedActiveScene(Scene current, Scene next){
        obstacle.enabled = true;
        Debug.Log("obstacle activated");
    }

}
