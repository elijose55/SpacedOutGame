using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrek : MonoBehaviour
{
    public GameObject target;
    public AudioSource music;
    public AudioSource deathSfx;
    public AudioSource shrekDeathSfx;
    public GameObject sEnd;
    public float speed = 30.0f;
    public float health = 100f;

    private Vector3 wayPointPos;
    private bool playerAlive = true;
    private bool shrekAlive = true;

    


    void Start()
    {
        target = GameObject.Find("Player");
        sEnd = GameObject.Find("EasterEgg/sEnd");
        
    }

    void Update()
    {
        if (health <= 0 && shrekAlive) {destroyed();}
        else if (health <= 0 && !shrekAlive) {transform.position = Vector3.MoveTowards(transform.position, sEnd.transform.position, 30 * Time.deltaTime);}
        else
        {
            wayPointPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, wayPointPos, speed * Time.deltaTime);
            transform.LookAt(target.transform);
        }
    }
    // Shrek destruido
    private IEnumerator shrekDeath()
    {
        target.GetComponent<Player>().scoreCounter += 20;
        yield return new WaitForSeconds(3.5f);
        Destroy(gameObject);
    }

    private void destroyed()
    {
        shrekAlive = false;
        shrekDeathSfx.Play();
        StartCoroutine(shrekDeath());
    }


    // Player destruido
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && playerAlive) {
            deathSfx.Play();
            target.GetComponent<Player>().currentHealth = 0;
            target.GetComponent<Player>().TakeDamage(0);
            music.Stop();
            playerAlive = false;
        }
    }
}
