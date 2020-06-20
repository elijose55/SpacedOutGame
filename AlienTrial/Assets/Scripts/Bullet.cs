using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 target;
    public bool targetSet;
    private float speed = 200;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (targetSet)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer ("Player")) {
            Destroy(gameObject);            
        }
    }
}
