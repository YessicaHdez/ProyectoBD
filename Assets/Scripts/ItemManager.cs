using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject invencibility;
    public GameObject extra_life;
    public GameObject ultra_jump;
    public GameObject restore_life;
    GameObject clone;

    void Start()
    {
        try
        {


            string sql = "SELECT * FROM ItemObject";

            using (SqlCommand command = new SqlCommand(sql, DbManager.instance.Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int no = reader.GetInt32(0);
                        int x = reader.GetInt32(1);
                        int y = reader.GetInt32(2);
                        int id = reader.GetInt32(3);
                        if (id == 1)
                        {
                            clone = Instantiate(invencibility);
                            Vector3 v = new Vector3(x, y);
                            clone.transform.position = v;
                            clone.GetComponent<ItemTypo>().oid = no;

                        }
                        else if (id == 2)
                        {
                            clone = Instantiate(extra_life);
                            Vector3 v = new Vector3(x, y);
                            clone.transform.position = v;
                            clone.GetComponent<ItemTypo>().oid = no;
                        }
                        else if (id == 3)
                        {
                            clone = Instantiate(ultra_jump);
                            Vector3 v = new Vector3(x, y);
                            clone.transform.position = v;
                            clone.GetComponent<ItemTypo>().oid = no;
                        }
                        else if (id == 4)
                        {
                            clone = Instantiate(restore_life);
                            Vector3 v = new Vector3(x, y);
                            clone.transform.position = v;
                            clone.GetComponent<ItemTypo>().oid = no;
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
