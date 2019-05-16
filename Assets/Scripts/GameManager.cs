using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] GameObject player;
    private bool gameOver = false;
    private static string playerName = "Test";
    public static string PlayerName { get { return playerName; } set { playerName = value;  } }

    public bool GameOver 
    {
        get => gameOver;
    }

    public GameObject Player 
    {
        get => player;
    }

    void Awake() 
    {
        if(instance == null) 
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerHit(int currentHP) 
    {
        if(currentHP > 0)
        {
            gameOver = false;
        }
        else
        {
            gameOver = true;
        }
    }
}
