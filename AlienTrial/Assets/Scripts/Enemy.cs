using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    //Variables
    public Transform target;
    private float dis;
    private float minDistance = 10f;
    public float speed = 4f;
    public AudioClip ShootClip;
    Rigidbody rig;
    private float RobotoBob = 0.01f;
    public GameObject bullet;
    
    public Transform shootPoint;
    public float shootSpeed = 10f;
    public float timeToShoot = 1.3f;
    float originalTime;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rig = GetComponent<Rigidbody>();
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = ShootClip;
        originalTime = timeToShoot;
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(transform.position, target.position);
        // Se estiver fora de range, perseguir jogador
        if (dis > minDistance)
        {
        pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
        rig.MovePosition(pos);
        }else
        {
            timeToShoot -= Time.deltaTime;
            if(timeToShoot < 0)
            {
                shootPlayer();
                timeToShoot = originalTime;
            }
        pos = transform.position;
        float newY = Mathf.Sin(Time.time * 5f) * RobotoBob + pos.y;
        float newX = Mathf.Sin(Time.time * 5f) * 0.01f + pos.x;
        transform.position = new Vector3(newX, newY, transform.position.z);

        }
        transform.LookAt(target);
    }


private void shootPlayer()
    {
        AudioSource.PlayClipAtPoint(ShootClip, shootPoint.position);
        GameObject currentBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody rig = currentBullet.GetComponent<Rigidbody>();

        rig.AddForce(transform.forward * shootSpeed, ForceMode.VelocityChange);
    }
}
