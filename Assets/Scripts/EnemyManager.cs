using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy01;
    public GameObject enemy02;
    GameObject clone;

    void Start()
    {
        try
        {


                string sql = "SELECT * FROM EnemyDestroyable"; 

                using (SqlCommand command = new SqlCommand(sql, DbManager.instance.Connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int damage = reader.GetInt32(1);
                            int health = reader.GetInt32(2);
                            int x = reader.GetInt32(3);
                            int y = reader.GetInt32(4);
                        int did = reader.GetInt32(5);
                        if (id == 1)
                            {
                            clone = Instantiate(enemy01);
                            Vector3 v = new Vector3(x, y);
                            clone.transform.position = v;
                            clone.GetComponent<EnemyTypo>().dId = did;

                            }
                            else if (id == 2)
                        {
                            clone = Instantiate(enemy02);
                            Vector3 v = new Vector3(x, y);
                            clone.transform.position = v;
                            clone.GetComponent<EnemyTypo>().dId = did;
                        }
                        }
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
        
    }
}
