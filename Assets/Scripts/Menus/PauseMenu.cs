using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//This script is of my own design

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    [SerializeField] SceneFader sceneFader;
    [SerializeField] GameController gc;
    private GameManager gm;

    [Header("Volume Controls")]
    [SerializeField] Slider master;
    [SerializeField] Slider music;
    [SerializeField] Slider sound;

    private void Start()
    {
        //Stops errors in editor due to no game manager being active yet
        if (GameObject.FindGameObjectsWithTag("GameManager").Length != 0)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
        
        master.value = PlayerPrefs.GetFloat(Discord.username + "_Master");
        music.value = PlayerPrefs.GetFloat(Discord.username + "_Music");
        sound.value = PlayerPrefs.GetFloat(Discord.username + "_Sound");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }

        PlayerPrefs.SetFloat(Discord.username + "_Master", master.value);
        PlayerPrefs.SetFloat(Discord.username + "_Music", music.value);
        PlayerPrefs.SetFloat(Discord.username + "_Sound", sound.value);


        if (GameObject.FindGameObjectsWithTag("GameManager").Length != 0)
        {
            gm.UpdateVolume();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
            Discord.AddToFile("Summary.txt", "Game Paused");
        }
        else
        {
            Time.timeScale = 1f;
            Discord.AddToFile("Summary.txt", "Game Unpaused");

        }
    }

    public void Retry()
    {
        Toggle();
        Discord.AddToFile("Summary.txt", "RESTARTING " + SceneManager.GetActiveScene().name.ToString() + " Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        sceneFader.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        MenuMusicManager.nextSceneIsMenu = true;

        //Stops any errors using it on tutorial level
        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            gc.UpdateDiscord();
            gc.UpdateDataRecorder();
        }
        else
        {
            Discord.AddToFile("Summary.txt", "LEAVING: Tutorial (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        }

        sceneFader.LoadScene("Menu_Main");
    }

}
