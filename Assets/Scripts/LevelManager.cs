using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public float timer = 0f;
    private float waitTime = 2.0f;

    public GameObject currentCheckpoint;
    public GameObject player;
    private PlayerHealth playerHealth;
    private CharacterMovement characterMovement;
    public Animator anim;
    private LifeManager lifeSystem;

    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        characterMovement = player.GetComponent<CharacterMovement>();
        anim = player.GetComponent<Animator>();
        lifeSystem = FindObjectOfType<LifeManager>();
    }

    void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        timer += Time.deltaTime;
        if(timer >= waitTime)
        {
            lifeSystem.TakeLife();
            player.transform.position = currentCheckpoint.transform.position;
            playerHealth.CurrentHealth = 100;
            timer = 0f;
            playerHealth.HealthSlider.value = playerHealth.CurrentHealth;
            characterMovement.enabled = true;
            anim.Play("Blend Tree");
        }
    }
}
