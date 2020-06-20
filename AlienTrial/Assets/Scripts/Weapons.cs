using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapons : MonoBehaviour
{

    public Gun[] loadout;
    public Transform weaponParent;
    public LayerMask worldLayer;
    public LayerMask Enemies;
    public LayerMask shrekLayer;
    public LayerMask bossLayer;
    public GameObject bulletPrefab;
    public GameObject bulletHolePrefab;
    public GameObject loothHealth;
    public GameObject Rifle;
    public AudioSource bulletSfx;
    public AudioSource equipSfx;
    public AudioSource shrekSfx;
    public bool rifleEnabled;
    private int currentIndex;
    private GameObject currentWeapon;
    private GameObject t_newHole;
    public GameObject deathVFX;
    private float lifeTime = 2;
    private float bulletSpeed = 0.0002f;
    private bool bulletTarget = false;
    private float nextTimeToFire = 0f;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        rifleEnabled = false;
        Equip(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) && rifleEnabled) Equip(1);

        if (currentIndex == 1 && !rifleEnabled) Equip(0);

        if (currentWeapon != null)
        {
            Aim(Input.GetMouseButton(1));
            if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
            
            {
                nextTimeToFire = Time.time + 1f / loadout[currentIndex].firerate;
                Shoot();
            }
        }
    }

    private IEnumerator bulletLife(GameObject bullet, float delay)
    {
        if (bullet != null)
        {
            yield return new WaitForSeconds(delay);
            Destroy(bullet);            
        }
    }

    private IEnumerator drawBulletHole(RaycastHit hit, float delay)
    {
        yield return new WaitForSeconds(delay);
        t_newHole = Instantiate (bulletHolePrefab, hit.point + hit.normal * 0.05f, Quaternion.identity) as GameObject;
        t_newHole.transform.LookAt(hit.point + hit.normal);
        Destroy(t_newHole, 5f); 
    }

    private IEnumerator disableRifle()
    {
        yield return new WaitForSeconds(10);
        rifleEnabled = false;
    }

    public void EnableRifle()
    {
        rifleEnabled = true;
        StartCoroutine(disableRifle());

    }

    public void Equip (int p_ind)
    {
        if (currentWeapon != null ) Destroy(currentWeapon);
        equipSfx.Stop();
        equipSfx.Play();

        GameObject t_newWeapon = Instantiate (loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newWeapon.transform.localPosition = Vector3.zero;
        t_newWeapon.transform.localEulerAngles = Vector3.zero;
        currentIndex = p_ind;
        currentWeapon = t_newWeapon;

    }

    void Aim (bool p_isAiming)
    {
        Transform t_anchor = currentWeapon.transform.Find("Anchor");
        Transform t_state_ads = currentWeapon.transform.Find("States/ADS");
        Transform t_state_hip = currentWeapon.transform.Find("States/Hip");

        if (p_isAiming)
        {
            //aim
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }
        else
        {
            //hip
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
        }

        //return p_isAiming;
        return;

    }

    void Shoot()
    {
        Transform t_spawn = transform.Find("Cameras/Main Camera");
        Transform bulletSpawn = currentWeapon.transform.Find("Anchor/Design/Barrel/BulletSpawn");


        RaycastHit hit = new RaycastHit();

        // Drones
        if (Physics.Raycast(t_spawn.position, t_spawn.forward, out hit, 1000f, Enemies))
        {

            // Explosão do drone
            var vfx = Instantiate(deathVFX, hit.point, Quaternion.identity);
            Destroy(vfx, 5f);
            Destroy(hit.collider.gameObject);
            gameObject.GetComponent<Player>().scoreCounter += 1;
            float randValue = Random.value;
            if(randValue< .10f)
            {

            Instantiate(loothHealth, hit.point, Quaternion.identity);
            }
            else if(randValue > .10f && randValue < .20f)
            {
                Instantiate(Rifle, hit.point, Quaternion.identity);
            }
        }
        
        // World layer
        else if (Physics.Raycast(t_spawn.position, t_spawn.forward, out hit, 1000f, worldLayer))
        {
            bulletTarget = true;
            float dist = Vector3.Distance(hit.point, bulletSpawn.position);
            //Sincronizar o BulletHole com o impacto do Bullet
            StartCoroutine(drawBulletHole(hit, dist/200));
        }

        // Easter Egg
        else if (Physics.Raycast(t_spawn.position, t_spawn.forward, out hit, 1000f, shrekLayer))
        {
            bulletTarget = true;
            hit.collider.gameObject.GetComponent<Shrek>().health -= 10;
            float dist = Vector3.Distance(hit.point, bulletSpawn.position);
            shrekSfx.Play();
            //Sincronizar o BulletHole com o impacto do Bullet
            //StartCoroutine(drawBulletHole(hit, dist/200));

        }

        // Boss
        else if (Physics.Raycast(t_spawn.position, t_spawn.forward, out hit, 1000f, bossLayer))
        {
            if(hit.collider.gameObject.GetComponent<EnemyBoss>().maxHealth <= 0)
            {
                // Explosão do drone
                var vfx = Instantiate(deathVFX, hit.point, Quaternion.identity);
                Destroy(vfx, 5f);
                Destroy(hit.collider.gameObject);
                gameObject.GetComponent<Player>().scoreCounter += 20;
            }
            else
            {

            bulletTarget = true;
            hit.collider.gameObject.GetComponent<EnemyBoss>().maxHealth -= 5;
            float dist = Vector3.Distance(hit.point, bulletSpawn.position);
            //shrekSfx.Play();
            //Sincronizar o BulletHole com o impacto do Bullet
            StartCoroutine(drawBulletHole(hit, dist / 200));
            }

        }

        // SFX
        bulletSfx.Stop();
        bulletSfx.clip = loadout[currentIndex].gunshotSound;
        bulletSfx.pitch = 1 - loadout[currentIndex].pitchRandomization + Random.RandomRange(-loadout[currentIndex].pitchRandomization, loadout[currentIndex].pitchRandomization);
        bulletSfx.volume = loadout[currentIndex].shotVolume;
        bulletSfx.Play();


        // Bullet Capsule effect
        if (bulletTarget == true)
        {
            Vector3 rotation = bulletSpawn.rotation.eulerAngles;
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(rotation.x - 90, transform.eulerAngles.y, rotation.z));
            bullet.GetComponent<Bullet>().target = hit.point;
            bullet.GetComponent<Bullet>().targetSet = true;
            StartCoroutine(bulletLife(bullet, lifeTime));
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);

            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), 
                                    bulletSpawn.parent.GetComponent<Collider>());

            bullet.transform.position = bulletSpawn.position;
            Vector3 rotation = bulletSpawn.rotation.eulerAngles;
            bullet.transform.rotation = Quaternion.Euler(rotation.x - 90, transform.eulerAngles.y, rotation.z);
            bullet.GetComponent<Rigidbody>().AddForce(t_spawn.forward * bulletSpeed, ForceMode.Impulse);
            StartCoroutine(bulletLife(bullet, lifeTime));
        }
        bulletTarget = false;

        

    }


}
