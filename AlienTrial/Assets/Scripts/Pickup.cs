using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Pickup : MonoBehaviour
{
    public float verticalBobFrequency = 1f;
    public float bobbingAmount = 1f;
    public float rotatingSpeed = 360f;
    public AudioClip pickupSFX;
    public GameObject pickupVFXPrefab;
    public UnityAction<Player> onPick;
    public Rigidbody pickupRigidbody { get; private set; }

    Collider m_Collider;
    Vector3 m_StartPosition;
    bool m_HasPlayedFeedback;

    private void Start()
    {
        pickupRigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();

        // ensure the physics setup is a kinematic rigidbody trigger
        //pickupRigidbody.isKinematic = true;
        //m_Collider.isTrigger = true;

        // Remember start position for animation
        m_StartPosition = transform.position;
    }

    private void Update()
    {
        // Handle bobbing
        //float bobbingAnimationPhase = ((Mathf.Sin(Time.time * verticalBobFrequency) * 0.5f) + 0.5f) * bobbingAmount;
        //transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;

        // Handle rotating
        transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Player pickingPlayer = other.GetComponent<Player>();


        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().pickedHealth = true;
            PlayPickupFeedback();
            Destroy(gameObject);
        }
    }

    public void PlayPickupFeedback()
    {
  
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
        var pickupVFXInstance = Instantiate(pickupVFXPrefab, transform.position, Quaternion.identity);

    }
}
