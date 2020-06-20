//using UnityEngine;

//public class HealthPickup : MonoBehaviour
//{
//    public float healAmount;

//    Pickup m_Pickup;

//    void Start()
//    {
//        m_Pickup = GetComponent<Pickup>();

//        // Subscribe to pickup action
//        m_Pickup.onPick += OnPicked;
//    }

//    void OnPicked(Player player)
//    {
//        Health playerHealth = player.GetComponent<Health>();
//        if (playerHealth && playerHealth.canPickup())
//        {
//            playerHealth.Heal(healAmount);

//            m_Pickup.PlayPickupFeedback();

//            Destroy(gameObject);
//        }
//    }
//}
