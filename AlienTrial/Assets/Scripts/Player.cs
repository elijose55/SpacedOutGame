using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

	private int maxHealth = 100;
	private bool dead;
	private bool climax = false;
	public int currentHealth;
	static public int score;
	static public int wave;
	public bool pickedHealth = false;
	public Text scoreText;
	public int scoreCounter = 0;

	public AudioSource normalMusic;
	public AudioSource gameOverSfx;

	public HealthBar healthBar;
	public GameObject gameOverImage;
	public GameObject droneManager;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		normalMusic.Play();
		dead = false;
		//gameOverImage = GameObject.Find("Canvas/GameOver").GetComponent<Image>();
	}


	void Update()
	{
		scoreText.text = scoreCounter.ToString("0");
		if (currentHealth <= 0 && !dead)
		{
			dead = true;
			gameOverSfx.Play();
			gameObject.GetComponent<Motion>().dead = true;
			gameOverImage.SetActive(true);
			normalMusic.Stop();
		
			score = scoreCounter;
			wave = droneManager.GetComponent<DroneManager>().waveCounter;
			StartCoroutine(gameOverTimer());
			
		}

        if(pickedHealth == true)
        {
			PickedHealth(20);
			pickedHealth = false;
        }
	}

    private IEnumerator gameOverTimer()
    {
        yield return new WaitForSeconds(6);
		SceneManager.LoadScene("Score");       
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
			Destroy(collision.gameObject);
			TakeDamage(20);

		}
    }

    public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}
	public void PickedHealth(int heal)
	{
		currentHealth += heal;

		healthBar.SetHealth(currentHealth);
	}
}