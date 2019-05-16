using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class AmmoTypo : MonoBehaviour
{
    public int id;
    public int damage;

    void Start()
    {
        try
        {
           using (SqlCommand command = new SqlCommand("SELECT dbo.GetDamage(@id)", DbManager.instance.Connection))
           { 
              command.Parameters.AddWithValue("@id", id);
              int valor = (int)command.ExecuteScalar();
              damage = valor;
           }
            
        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }
    }

    void Update()
    {
        
    }
}
