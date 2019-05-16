using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using UnityEngine.UI;

public class StatScript : MonoBehaviour
{
    private float timeBetween = 0.2f;
    private float timer = 0f;
    public GameObject statScreen;
    private bool active = false;

    public int score;
    public int killcount;

    public Text t1;
    public Text t2;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Stats") && timer > timeBetween)
        {
            statScreen.SetActive(!active);
            active = !active;
            timer = 0;
            RetrieveData();
        }
    }

    void RetrieveData()
    {
        try
        {
            using (SqlCommand command = new SqlCommand("SELECT dbo.KillCount(@PlayerName)", DbManager.instance.Connection))
            {
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                int valor = (int)command.ExecuteScalar();
                killcount = valor;
            }
            using (SqlCommand command = new SqlCommand("SELECT dbo.Score(@PlayerName)", DbManager.instance.Connection))
            {
                command.Parameters.AddWithValue("@PlayerName", GameManager.PlayerName);
                int valor = (int)command.ExecuteScalar();
                score = valor;
            }
            UpdateUI();
        }
        catch (SqlException e)
        {
            Debug.Log(e.ToString());
        }
    }

    void UpdateUI()
    {
        t1.text = killcount + " Kills";
        t2.text = "Score: " + score + " points";
    }
}
