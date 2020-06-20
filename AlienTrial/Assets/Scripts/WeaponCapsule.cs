using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCapsule : MonoBehaviour
{

    public AudioSource captureSfx;
    public GameObject player;
    private bool captured;
    void Start()
    {
        captured = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, 70 * Time.deltaTime);
        
    }


    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !captured) {
            captureSfx.Play();
            player.GetComponent<Weapons>().EnableRifle();
            player.GetComponent<Weapons>().Equip(1);
            captured = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, 2f);
        }
    }
}
