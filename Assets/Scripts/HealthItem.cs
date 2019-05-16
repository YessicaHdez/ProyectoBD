using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth playerHealth;
    private PowerItemExplode powerItemExplode;
    
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        powerItemExplode = GetComponent<PowerItemExplode>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            playerHealth.PowerUpHealth();
            powerItemExplode.Pickup();
            Destroy(gameObject);
        }
    }
}
