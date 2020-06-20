using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBullet : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Tip());
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    IEnumerator Tip()
    {


        yield return new WaitForSeconds(5);
        Destroy(gameObject);

    }
}
