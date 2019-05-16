using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour
{
    private GameObject player;
    private AudioSource audio;
    public AudioClip pick;
    private CharacterMovement mov;
    private PowerItemExplode powerItemExplode;
    private SphereCollider sphereCollider;

    void Start()
    {
        player = GameManager.instance.Player;
        mov = player.GetComponent<CharacterMovement>();
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
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            audio.PlayOneShot(pick);
            StartCoroutine(SuperJumpRoutine());
        }
    }

    public IEnumerator SuperJumpRoutine()
    {
        powerItemExplode.Pickup();
        sphereCollider.enabled = false;
        float speed = mov.jumpSpeed;
        mov.jumpSpeed = speed * 2;
        yield return new WaitForSeconds(10f);
        mov.jumpSpeed = speed;
        Destroy(gameObject);
    }

}
