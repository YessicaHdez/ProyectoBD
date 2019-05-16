using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float timeBetweenAttacks = 0.5f;
    private float timer = 0f;
    private float timer2 = 0f;
    [SerializeField] private float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection;
    new Rigidbody rigidbody;
    private Animator animator;

    public float jumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float swordSpeed = 600.0f;
    public Transform swordSpawn;
    public Rigidbody swordPrefab;
    public Rigidbody arrowPrefab;
    private AudioSource audio;
    public AudioClip jump;
    public AudioClip attackAudio;

    Rigidbody clone;

    public float syncLatency = 5f;

    void Awake()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
        swordSpawn = GameObject.Find("SwordSpawn").transform;
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        try
        {

            using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.LastPosition(@PlayerName)", DbManager.instance.Connection))
            {
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int x = -53;
                    int y = 0;
                    
                    while (reader.Read())
                    {
                        x = reader.GetInt32(0);
                        y = reader.GetInt32(1);
                        Debug.Log(x + " " + y);

                    }
                    Vector3 pos = new Vector3(x, y);
                    transform.position = pos;
                }
            }

        }
        catch (SqlException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        moveDirection = Input.GetAxis("Horizontal");

        if(grounded && Input.GetButton("Jump"))
        {
            animator.SetTrigger("isJumping");
            rigidbody.AddForce(new Vector2(0, jumpSpeed));
            audio.PlayOneShot(jump);
        }

        if((Input.GetMouseButtonDown(0) || Input.GetAxis("XboxTrigger1") > 0.1) && timer > timeBetweenAttacks )
        {
            Attack();
            timer = 0;
        }

        if ((Input.GetMouseButtonDown(1) || Input.GetAxis("XboxTrigger2") > 0.1) && timer > timeBetweenAttacks)
        {
            Attack2();
            timer = 0;
        }

        if(timer2 > syncLatency)
        {
            timer2 = 0;
            ReportPosition();
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        rigidbody.velocity = new Vector2(moveDirection * maxSpeed, rigidbody.velocity.y);
  
        if(moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if (moveDirection < 0.0f && facingRight)
        {
            Flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    void Attack()
    {
        if(AmmoManager.instance.AvailableAmmo(AmmoType.Sword))
        {
            animator.SetTrigger("isAttacking");
            audio.PlayOneShot(attackAudio);
            clone = Instantiate(swordPrefab, swordSpawn.position, swordSpawn.rotation) as Rigidbody;
            clone.AddForce(swordSpawn.transform.right * swordSpeed);
            AmmoManager.instance.Fire(AmmoType.Sword);
        }
    }

    void Attack2()
    {
        if (AmmoManager.instance.AvailableAmmo(AmmoType.Arrow))
        {
            animator.SetTrigger("isAttacking");
            audio.PlayOneShot(attackAudio);
            clone = Instantiate(arrowPrefab, swordSpawn.position, swordSpawn.rotation) as Rigidbody;
            clone.AddForce(swordSpawn.transform.right * swordSpeed);
            AmmoManager.instance.Fire(AmmoType.Arrow);
        }
    }

    void ReportPosition()
    {
        try
        {
            using (SqlCommand command = new SqlCommand("dbo.ReportPosition", DbManager.instance.Connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                command.Parameters.AddWithValue("@X", transform.position.x);
                command.Parameters.AddWithValue("@Y", transform.position.y);
                command.ExecuteNonQuery();
            }
        }
        catch (SqlException e)
        {
            Debug.LogError(e.ToString());
        }
    }


}
