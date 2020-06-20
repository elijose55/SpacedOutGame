using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyBomber : MonoBehaviour
{
    //Variables
    public Transform target;
    
    public float speed = 4f;
    public AudioClip ShootClip;
    Rigidbody rig;
    
    public GameObject deathVFX;


    
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rig = GetComponent<Rigidbody>();
        AudioSource audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        transform.LookAt(target);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        // Explosão do drone
        var vfx = Instantiate(deathVFX, gameObject.transform.position, Quaternion.identity);
        Destroy(vfx, 5f);
        if(collision.gameObject.tag == "Player")
        {

        collision.gameObject.GetComponent<Player>().TakeDamage(20);
        }
    }

}
