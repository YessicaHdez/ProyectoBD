using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;

struct Ammo
{
    public int Swords;
    public int Arrows;
}

public enum AmmoType
{
    Sword, Arrow
}

public class AmmoManager : MonoBehaviour
{

    public static AmmoManager instance = null;

    private Ammo inventory;
    private Ammo used;
    [SerializeField] private Text swords;
    [SerializeField] private Text arrows;

    void Start()
    {
        InitializeAmmo();
        UpdateCounter();
    }

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

    void InitializeAmmo()
    {
        try
        {

            using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.GetAmmo(@PlayerName)", DbManager.instance.Connection))
            {
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int current = reader.GetInt32(1);
                        int used = reader.GetInt32(2);
                        switch (id)
                        {
                            case 1:
                                inventory.Arrows = current;
                                this.used.Arrows = used;
                                break;
                            case 2:
                                inventory.Swords = current;
                                this.used.Swords = used;
                                break;
                        }
                        i++;
                    }
                    if (i == 0)
                    {
                        using (SqlCommand command2 = new SqlCommand("dbo.RegisterUser", DbManager.instance.Connection))
                        {
                            command2.CommandType = System.Data.CommandType.StoredProcedure;
                            command2.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                            command2.ExecuteNonQuery();
                        }
                        InitializeAmmo();
                    }


                }
            }

        }
        catch(SqlException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    void UpdateCounter()
    {
        swords.text = "x" + inventory.Swords;
        arrows.text = "x" + inventory.Arrows;
    }

    void Sync()
    {
        try
        {
            DbManager.instance.Connection.Open();
            using (SqlCommand command2 = new SqlCommand("dbo.UpdateAmmo", DbManager.instance.Connection))
            {
                command2.CommandType = System.Data.CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                command2.Parameters.AddWithValue("@AmmoID", 1);
                command2.Parameters.AddWithValue("@Current", inventory.Arrows);
                command2.Parameters.AddWithValue("@Used", used.Arrows);
                command2.ExecuteNonQuery();
            }
            using (SqlCommand command2 = new SqlCommand("dbo.UpdateAmmo", DbManager.instance.Connection))
            {
                command2.CommandType = System.Data.CommandType.StoredProcedure;
                command2.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                command2.Parameters.AddWithValue("@AmmoID", 2);
                command2.Parameters.AddWithValue("@Current", inventory.Swords);
                command2.Parameters.AddWithValue("@Used", used.Swords);
                command2.ExecuteNonQuery();
            }
            DbManager.instance.Connection.Close();
        }
        catch(SqlException e)
        {
            Debug.LogError(e.ToString());
        }
    }

    void OnDestroy()
    {
        Sync();
    }

    public void Fire(AmmoType t)
    {
        switch(t)
        {
            case AmmoType.Sword:
                inventory.Swords--;
                used.Swords++;
                break;
            case AmmoType.Arrow:
                inventory.Arrows--;
                used.Arrows++;
                break;
        }
        UpdateCounter();
    }

    public bool AvailableAmmo(AmmoType t)
    {
        switch (t)
        {
            case AmmoType.Sword:
                return inventory.Swords > 0;
            case AmmoType.Arrow:
                return inventory.Arrows > 0;
        }
        return false;
    }

    void Update()
    {
        
    }

    
}
