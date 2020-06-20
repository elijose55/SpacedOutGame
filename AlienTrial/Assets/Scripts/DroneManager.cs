using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneManager : MonoBehaviour
{
    public float timeToInstantiate = 5f;
    public float timeToInstantiateBombers = 20f;
    float originalTime;
    float originalTimeBombers;
    public int waveCounter = 0;
    public Text waveText;
    public Text ETAText;
    public Text BigWave;

    public GameObject Drone;
    public GameObject DronePearlHarbor;
    public GameObject DroneBoss;
    // Start is called before the first frame update
    void Start()
    {
        originalTime = timeToInstantiate;
        originalTimeBombers = timeToInstantiateBombers;
        InstantiateDrones(5);
        waveCounter += 1;

    }

    // Update is called once per frame
    void Update()
    {
        timeToInstantiate -= Time.deltaTime;
        //timeToInstantiateBombers -= Time.deltaTime;
        //if(waveCounter%5 == 0)
        //{
        //    Instantiate(DroneBoss, RandomPointOnCircleEdge(60f,15), Quaternion.identity, gameObject.transform);
            
        //}
        if(waveCounter <= 3)
        {
        if (timeToInstantiate < 0)
        {
            InstantiateDrones(waveCounter*5);
            timeToInstantiate = originalTime;
            waveCounter += 1;
        }

        }
        else if (waveCounter > 3)
        {
            if (timeToInstantiate < 0)
            {
                if(waveCounter%5 == 0)
                {
                    //Instantiate(DroneBoss, RandomPointOnCircleEdge(60f, 15), Quaternion.identity, gameObject.transform);
                    InstantiateBoss(waveCounter / 5 - 1);
                }
                InstantiateBombers(2 * waveCounter);
                //timeToInstantiateBombers = originalTimeBombers;
                InstantiateDrones(waveCounter * 3);
                timeToInstantiate = originalTime;
                waveCounter += 1;
            }

        }
        waveText.text = (waveCounter-1).ToString("0");
        ETAText.text = timeToInstantiate.ToString();
        if(waveCounter%5 ==0 && timeToInstantiate < 6)
        {
            BigWave.enabled = true;
        }
        else
        {
            BigWave.enabled = false;
        }


    }
    private void InstantiateDrones(int n_drones)
    {
        for (int i = 0; i < n_drones; i++)
        {
            Instantiate(Drone, RandomPointOnCircleEdge(60f,7), Quaternion.identity, gameObject.transform);
        }
    }
    private void InstantiateBombers(int n_bombers)
    {
        for (int i = 0; i <= n_bombers; i++)
        {
            Instantiate(DronePearlHarbor, RandomPointOnCircleEdge(60f,7), Quaternion.identity, gameObject.transform);
        }
    }
    private void InstantiateBoss(int n_boss)
    {
        for (int i = 0; i <= n_boss; i++)
        {
            Instantiate(DroneBoss, RandomPointOnCircleEdge(60f, 15), Quaternion.identity, gameObject.transform);
        }
    }
    private Vector3 RandomPointOnCircleEdge(float radius, float height)
    {
        var vector2 = Random.insideUnitCircle.normalized * radius;
        return new Vector3(vector2.x, height, vector2.y);
    }
}
