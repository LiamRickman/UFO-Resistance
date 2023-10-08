using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

//This is a script of my own making.
public class GameManager : MonoBehaviour
{
    public GameManager m_Instance;

    private GameObject[] musicAS;
    private GameObject[] soundAS;
    private SceneFader sceneFader;
    private float sendDelay = 0.5f;

    void Awake()
    {
        GameObject[] l_GameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (l_GameManagers.Length > 1)
        {
            m_Instance = null;
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        m_Instance = this;
        Application.wantsToQuit += WantsToQuit;

        UpdateVolume();
    }

    bool WantsToQuit()
    {
        Debug.Log("Player prevented from quitting.");
        m_Instance.StartCoroutine(SubmitFiles());
        
        return false;
    }

    IEnumerator SubmitFiles()
    {
        if (SceneManager.GetActiveScene().name != "Menu_DataAgreement")
        {
            sceneFader = GameObject.FindGameObjectWithTag("SceneFader").GetComponent<SceneFader>();
            sceneFader.LoadScene("Menu_Quit");

            //Adds quit game 
            Discord.AddToFile("Summary.txt", "QUITTING GAME #" + PlayerPrefs.GetInt(Discord.username + "_SessionID") + " (Time Played: " + Time.timeSinceLevelLoad.ToString("F2") + ")");

            Discord.AddFileToForm("Summary.txt");
            Discord.AddFileToForm("Level 01.csv");
            Discord.AddFileToForm("Level 02.csv");
            Discord.AddFileToForm("Level 03.csv");
            Discord.AddFileToForm("Level 04.csv");
            Discord.AddFileToForm("Level 05.csv");
            Discord.SendFormToDiscord();
            yield return new WaitForSeconds(sendDelay);


            Discord.AddFileToForm("Level 01_HM_EnemyDeaths.txt");
            Discord.AddFileToForm("Level 02_HM_EnemyDeaths.txt");
            Discord.AddFileToForm("Level 03_HM_EnemyDeaths.txt");
            Discord.AddFileToForm("Level 04_HM_EnemyDeaths.txt");
            Discord.AddFileToForm("Level 05_HM_EnemyDeaths.txt");

            Discord.AddFileToForm("Level 01_HM_Turrets.txt");           
            Discord.AddFileToForm("Level 02_HM_Turrets.txt");
            Discord.AddFileToForm("Level 03_HM_Turrets.txt");
            Discord.AddFileToForm("Level 04_HM_Turrets.txt");
            Discord.AddFileToForm("Level 05_HM_Turrets.txt");
            Discord.SendFormToDiscord();
            yield return new WaitForSeconds(sendDelay);
        }

        Application.wantsToQuit -= WantsToQuit;
        Application.Quit();
    }

    public void UpdateVolume()
    {

        //Audio Controls
        musicAS = GameObject.FindGameObjectsWithTag("Music");
        soundAS = GameObject.FindGameObjectsWithTag("Sound");

        float musicVolume = PlayerPrefs.GetFloat(Discord.username + "_Master") * PlayerPrefs.GetFloat(Discord.username + "_Music");
        float soundVolume = PlayerPrefs.GetFloat(Discord.username + "_Master") * PlayerPrefs.GetFloat(Discord.username + "_Sound");

        if (musicAS.Length > 0)
        {
            for (int i = 0; i < musicAS.Length; i++)
            {
                musicAS[i].GetComponent<AudioSource>().volume = musicVolume;
            }
        }

        if (soundAS.Length > 0)
        {
            for (int i = 0; i < soundAS.Length; i++)
            {
                soundAS[i].GetComponent<AudioSource>().volume = soundVolume;
            }
        }

        

    }
}
