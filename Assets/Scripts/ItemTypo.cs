using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class ItemTypo : MonoBehaviour
{

    public int oid;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == GameManager.instance.Player)
        {
            ReportInteraction();
        }
    }

    void ReportInteraction()
    {
        try
        {
            using (SqlCommand command = new SqlCommand("dbo.RegisterInteraction", DbManager.instance.Connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                command.Parameters.AddWithValue("@X", transform.position.x);
                command.Parameters.AddWithValue("@Y", transform.position.y);
                command.Parameters.AddWithValue("@OId", oid);
                command.ExecuteNonQuery();
            }
        }
        catch (SqlException e)
        {
            Debug.LogError(e.ToString());
        }
    }
}
