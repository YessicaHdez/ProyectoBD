using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : MonoBehaviour
{
    private GameObject player;
    private AudioSource audio;
    //private Invincible invincible;
    public AudioClip pick;
    private PlayerHealth playerHealth;
    private ParticleSystem particleSystem;
    private ParticleSystem brainParticles;
    public GameObject pickupEffect;
    private PowerItemExplode powerItemExplode;
    private SphereCollider sphereCollider;

    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;
        particleSystem = player.GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
        brainParticles = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
        powerItemExplode = GetComponent<PowerItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            audio.PlayOneShot(pick);
            StartCoroutine(InvincibleRoutine());
        }
    }

    public IEnumerator InvincibleRoutine()
    {
        powerItemExplode.Pickup();
        particleSystem.enableEmission = true;
        playerHealth.enabled = false;
        brainParticles.enableEmission = false;
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(10f);
        particleSystem.enableEmission = false;
        playerHealth.enabled = true;
        Destroy(gameObject);
    }

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
    }
}
