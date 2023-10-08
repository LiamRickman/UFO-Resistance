using UnityEngine;
using UnityEngine.UI;

//This script is of my own design.

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;
    private GameManager gm;


    [Header("Setting Panels")]
    [SerializeField] GameObject gameplayPanel;
    [SerializeField] GameObject controlsPanel;
    [SerializeField] GameObject graphicsPanel;
    [SerializeField] GameObject audioPanel;
    [SerializeField] GameObject accessibilityPanel;

    [Header("Buttons")]
    [SerializeField] Button audioButton;
    [SerializeField] Button controlsButton;

    [Header("Audio References")]
    [SerializeField] Slider master;
    [SerializeField] Slider music;
    [SerializeField] Slider sound;


    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        Discord.AddToFile("Summary.txt", "LOADING: Settings Menu");

        //Sets Default Menu
        controlsButton.Select();
        PressControls();


        master.value = PlayerPrefs.GetFloat(Discord.username + "_Master");
        music.value = PlayerPrefs.GetFloat(Discord.username + "_Music");
        sound.value = PlayerPrefs.GetFloat(Discord.username + "_Sound");
    }

    private void Update()
    {
        PlayerPrefs.SetFloat(Discord.username + "_Master", master.value);
        PlayerPrefs.SetFloat(Discord.username + "_Music", music.value);
        PlayerPrefs.SetFloat(Discord.username + "_Sound", sound.value);

        gm.UpdateVolume();
    }

    public void Menu()
    {
        Discord.AddToFile("Summary.txt", "LEAVING: Settings Menu (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + "s)");

        sceneFader.LoadScene("Menu_Main");
    }

    public void PressGameplay()
    {
        gameplayPanel.SetActive(true);
        controlsPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        accessibilityPanel.SetActive(false);
    }

    public void PressControls()
    {
        gameplayPanel.SetActive(false);
        controlsPanel.SetActive(true);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        accessibilityPanel.SetActive(false);
    }

    public void PressGraphics()
    {
        gameplayPanel.SetActive(false);
        controlsPanel.SetActive(false);
        graphicsPanel.SetActive(true);
        audioPanel.SetActive(false);
        accessibilityPanel.SetActive(false);
    }

    public void PressAudio()
    {
        gameplayPanel.SetActive(false);
        controlsPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(true);
        accessibilityPanel.SetActive(false);
    }

    public void PressAccessibility()
    {
        gameplayPanel.SetActive(false);
        controlsPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audioPanel.SetActive(false);
        accessibilityPanel.SetActive(true);
    }

}
