 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient; 

public class DbManager : MonoBehaviour
{
    public static DbManager instance = null;

    private SqlConnection connection;
    private bool init = false;
    public SqlConnection Connection
    {
        get
        {
            if(!init)
            {
                Initialize();
            }
            return connection;
        }
    }

    private const string ConnectionString = @"Server=localhost\SQLExpress;Database=GAME_PROJECT_DB;User Id=api;Password=12345678";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }   
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Initialize()
    {
        try
        {
            connection = new SqlConnection(ConnectionString);
            connection.Open();
            init = true;
        }
        catch (SqlException e)
        {
            init =  false;
            Debug.LogError(e.ToString());
        }
    }

    void Start()
    {
        Initialize();
        try
        {
            using (SqlCommand command = new SqlCommand("dbo.RegisterUser", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                command.ExecuteNonQuery();
            }
        }
        catch(SqlException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        Connection.Close();
    }
}
