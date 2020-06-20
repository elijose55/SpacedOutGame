using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string name;
    public int damage;
    public int firerate;
    public float aimSpeed;
    public AudioClip gunshotSound;
    public float pitchRandomization;
    public float shotVolume;
    public GameObject prefab;
    public GameObject display;
}
