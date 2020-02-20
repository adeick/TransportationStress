using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffice : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 60f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
