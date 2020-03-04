using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSetting : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 60f;
    [SerializeField] private GameObject subway;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SubwayEnable", destroyDelay);
        Destroy(gameObject, destroyDelay);
    }
    void SubwayEnable() {
        subway.SetActive(true);
    }
}

