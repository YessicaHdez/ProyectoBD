using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;

    public float arrowSpeed = 600.0f;
    public Transform arrowSpawn;
    public Rigidbody arrowPrefab;
    Rigidbody clone;

    void Start()
    {
        arrowSpawn = GameObject.Find("ArrowSpawn").transform;
        anim = GetComponent<Animator>();
        player = GameManager.instance.Player;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            anim.SetTrigger("isAttacking");
        }
    }

    public void FireArcherProyectile()
    {
        clone = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as Rigidbody;
        clone.AddForce(arrowSpawn.transform.forward * arrowSpeed);
    }
}
