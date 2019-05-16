using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public InputField input;

    void Start()
    {
        input = FindObjectOfType<InputField>();
    }

    void Update()
    {
        //Debug.Log(input.text);
    }

    public void PlayGame()
    {
        if(input.text != "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //Debug.Log(input.text);
            GameManager.PlayerName = input.text;

        }
    }

    public void QuitGame()
    {
        print("Quit game");
        Application.Quit();
    }
}
