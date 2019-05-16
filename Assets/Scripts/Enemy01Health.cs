using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.AI;

public class Enemy01Health : MonoBehaviour
{

    [SerializeField] private int startingHealth = 20;
    [SerializeField] private float timeSinceLastHit = 0.5f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currrentHealth;
    private float timer = 0f;
    private Animator animator;
    private NavMeshAgent nav;
    private bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool dissapearEnemy = false;
    private BoxCollider weaponCollider;
    private DropItems dropItems;

    int id;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    void Start()
    {
        EnemyTypo e = GetComponent<EnemyTypo>();
        startingHealth = e.health;
        id = e.id;
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        isAlive = true;
        currrentHealth = startingHealth;
        weaponCollider = GetComponentInChildren<BoxCollider>();
        dropItems = GetComponent<DropItems>();    
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "PlayerWeapon")
            {
                AmmoTypo t = other.GetComponent<AmmoTypo>(); 
                TakeHit(t.damage, t);
          
                timer = 0f;
            }
        }
    }

    void TakeHit(int damage, AmmoTypo t)
    {
        if(currrentHealth > 0)
        {
            animator.Play("Hit");
            currrentHealth -= damage;
        }
        if(currrentHealth <= 0)
        {
            isAlive = false;
            KillEnemy(t);
        }
    }

    void KillEnemy(AmmoTypo t)
    {
        ReportKill(t.id);
        capsuleCollider.enabled = false;
        nav.enabled = false;
        animator.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        weaponCollider.enabled = false;
        StartCoroutine(RemoveEnemy());
        dropItems.Drop();
    }

    void ReportKill(int i)
    {
        try
        {
            using (SqlCommand command = new SqlCommand("dbo.KillEnemy", DbManager.instance.Connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DId", GetComponent<EnemyTypo>().dId);
                command.Parameters.AddWithValue("@X", transform.position.x);
                command.Parameters.AddWithValue("@Y", transform.position.y);
                command.Parameters.AddWithValue("@AmmoId", i);
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                command.ExecuteNonQuery();
            }
        }
        catch (SqlException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        dissapearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
