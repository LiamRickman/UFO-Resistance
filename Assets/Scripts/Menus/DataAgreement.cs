using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is of my own making.

public class DataAgreement : MonoBehaviour
{
    [SerializeField] Button continueButton;
    [SerializeField] Toggle toggleButton;
    [SerializeField] Scrollbar scrollbar;

    [SerializeField] SceneFader sceneFader;
    [SerializeField] InputField username;

    [SerializeField] Toggle returning;

    private bool formRead = false;
    private int playCount;
    private bool returningPlayer = false;

    private string startTime;

    private int sessionCount;

    private void Awake()
    {
        Discord.username = PlayerPrefs.GetString(("Username"));
    }

    private void Start()
    {
        //Show current username in box
        if (PlayerPrefs.GetString("Username") != null)
        {
            username.text = PlayerPrefs.GetString("Username");
        }

        //Stores the time the consent form was opened as otherwise it shows as when the continue button is pressed.
        startTime = "[" + System.DateTime.Now.ToString() + "] ";

        scrollbar.value = 1;

        toggleButton.interactable = false;
        toggleButton.isOn = false;
    }

    private void Update()
    {
        if (formRead)
            toggleButton.interactable = true;
        else
            toggleButton.interactable = false;

        if (scrollbar.value <= 0)
            formRead = true;

        if (returning.isOn)
            returningPlayer = true;
        else
            returningPlayer = false;

        if (toggleButton.isOn && username.text.Length > 1 && username.text.Length < 15)
            continueButton.interactable = true;
        else
            continueButton.interactable = false;
    }

    public void PressContinue()
    {
        //Updates player username
        PlayerPrefs.SetString("Username", username.text);
        Discord.username = PlayerPrefs.GetString(("Username"));

        if (!Discord.FolderExists(Discord.username))
        {
            sessionCount = 1;
            PlayerPrefs.SetInt(Discord.username + "_SessionID", 1);
            PlayerPrefs.SetFloat(Discord.username + "_Master", 0.5f);
            PlayerPrefs.SetFloat(Discord.username + "_Music", 0.5f);
            PlayerPrefs.SetFloat(Discord.username + "_Sound", 0.5f);
        }
        else
        {
            sessionCount = PlayerPrefs.GetInt(Discord.username + "_SessionID");
            sessionCount++;
            PlayerPrefs.SetInt(Discord.username + "_SessionID", sessionCount);
        }

        //Clearing files as old files are sent to discord anyway as a backup. Ensures fresh data every time
        ClearFiles();

        Discord.AddToFile("Summary.txt", "Username: " + PlayerPrefs.GetString("Username"), false);

        Discord.AddToFile("Summary.txt", "User ID: " + PlayerPrefs.GetString("unity.cloud_userid_h2665564582"), false);

        //Returning Player
        Discord.AddToFile("Summary.txt", "Played Previous Build: " + returningPlayer.ToString(), false);

        Discord.AddToFile("Summary.txt", "Completed Tutorial: No", false);

        Discord.AddToFile("Summary.txt", "Session Count: " + sessionCount, false);
        Discord.AddToFile("Summary.txt", "Version Played: " + Application.version, false);

        Discord.AddToFile("Summary.txt", "", false);

        Discord.AddToFile("Summary.txt", startTime + "STARTED GAME #" + sessionCount, false);
        Discord.AddToFile("Summary.txt", startTime + "LOADED: Consent Form", false);
        Discord.AddToFile("Summary.txt", "LEAVING: Consent Form" + " (Time Played: " + Time.timeSinceLevelLoad.ToString("F2") + ")");

        sceneFader.LoadScene("Menu_Main");
    }

    public void PressExit()
    {
        Application.Quit();
    }

    private void ClearFiles()
    {
        Discord.ClearFile("Summary.txt");

        Discord.ClearFile("Level 01.csv");
        Discord.ClearFile("Level 02.csv");
        Discord.ClearFile("Level 03.csv");
        Discord.ClearFile("Level 04.csv");
        Discord.ClearFile("Level 05.csv");

        Discord.ClearFile("Level 01_HM_EnemyDeaths.txt");
        Discord.ClearFile("Level 02_HM_EnemyDeaths.txt");
        Discord.ClearFile("Level 03_HM_EnemyDeaths.txt");
        Discord.ClearFile("Level 04_HM_EnemyDeaths.txt");
        Discord.ClearFile("Level 05_HM_EnemyDeaths.txt");

        Discord.ClearFile("Level 01_HM_Turrets.txt");
        Discord.ClearFile("Level 02_HM_Turrets.txt");
        Discord.ClearFile("Level 03_HM_Turrets.txt");
        Discord.ClearFile("Level 04_HM_Turrets.txt");
        Discord.ClearFile("Level 05_HM_Turrets.txt");
    }
}
