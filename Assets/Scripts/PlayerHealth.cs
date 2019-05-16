using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2.0f;
    [SerializeField] Slider healthSlider;

    private CharacterMovement characterMovement;
    private float timer = 0f;
    private Animator anim;
    [SerializeField] private int currentHealth;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip pickItem;

    public LevelManager levelManager;
    public bool isDead;

    public int CurrentHealth {
        get { return currentHealth; }
        set {
            if(value < 0)
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth = value;
            }
        }
    }

    public Slider HealthSlider {
        get 
        {
            return healthSlider;
        }
    }

    void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        characterMovement = GetComponent<CharacterMovement>();
        audio = GetComponent<AudioSource>();
        levelManager = FindObjectOfType<LevelManager>();
        isDead = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        PlayerKill();
    }

    void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "Weapon")
                
            {
                EnemyTypo e = other.GetComponent<EnemyTypo>();
                print("Collision HIt");
                TakeHit(e.damage);
                timer = 0;
            }
        }
    }

    void TakeHit(int d)
    {
        if(currentHealth > 0)
        {           
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= d;
            healthSlider.value = currentHealth;
            audio.PlayOneShot(hurtAudio);
        }
        if (currentHealth <= 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.SetTrigger("isDead");
            characterMovement.enabled = false;
        }
    }

    public void PowerUpHealth()
    {
        if(currentHealth <= 80)
        {
            CurrentHealth += 20;
        }
        else if (currentHealth < startingHealth)
        {
            CurrentHealth = startingHealth;
        }
        healthSlider.value = currentHealth;
        audio.PlayOneShot(pickItem);
    }

    public void KillBox()
    {
        CurrentHealth = 0;
        healthSlider.value = currentHealth;
    }

    public void PlayerKill()
    {
        if(currentHealth <= 0)
        {
            characterMovement.enabled = false;
            levelManager.RespawnPlayer();
        }
    }
}
