using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class EnemyTypo : MonoBehaviour
{
    public int id;
    public int health;
    public int damage;
    public int dId;

    void Start()
    {
        try
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.GetEnemyStats(@Id)", DbManager.instance.Connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        damage = reader.GetInt32(0);
                        health = reader.GetInt32(1);
                    }
                }
            }

        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
