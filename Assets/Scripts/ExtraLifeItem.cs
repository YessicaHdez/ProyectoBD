using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeItem : MonoBehaviour
{
    private GameObject player;
    private AudioSource audio;
    public AudioClip pick;
    private PowerItemExplode powerItemExplode;
    private SphereCollider sphereCollider;

    private LifeManager lifeSystem;
    

    void Start()
    {
        player = GameManager.instance.Player;
        audio = GetComponent<AudioSource>();
        powerItemExplode = GetComponent<PowerItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
        lifeSystem = FindObjectOfType<LifeManager>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            audio.PlayOneShot(pick);
            lifeSystem.GiveLife();
            powerItemExplode.Pickup();
            sphereCollider.enabled = false;
            Destroy(gameObject);
        }
    }

}
