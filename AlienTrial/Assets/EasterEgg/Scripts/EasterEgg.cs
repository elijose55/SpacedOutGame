using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{

    public GameObject shrekPrefab;
    public Transform shrekSpawn;
    private bool activated = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (!activated)
        {
            activated = true;
            GameObject shrek = Instantiate(shrekPrefab, shrekSpawn.position, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z));
        }
    }
}
