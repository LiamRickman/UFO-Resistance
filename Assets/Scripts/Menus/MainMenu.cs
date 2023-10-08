using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script is of my own making.
public class MainMenu : MonoBehaviour
{
    private GameManager gm;

    [SerializeField] SceneFader sceneFader;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.UpdateVolume();

        Discord.AddToFile("Summary.txt", "LOADING: Main Menu");
    }

    public void Play()
    {
        Discord.AddToFile("Summary.txt", "LEAVING: Main Menu (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        sceneFader.LoadScene("Menu_LevelSelect");
    }

    public void Settings()
    {
        Discord.AddToFile("Summary.txt", "LEAVING: Main Menu (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        sceneFader.LoadScene("Menu_Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
