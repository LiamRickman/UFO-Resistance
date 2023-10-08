using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//This script is of my own design and controls the round status for the game. 

public class RoundControls : MonoBehaviour
{
    [SerializeField] Button play;
    [SerializeField] Button fast;

    [SerializeField] Color selectedColour;

    private RoundSpawner roundSpawner;

    [SerializeField] Tutorial tutorial;

    private void Start()
    {
        roundSpawner = GameObject.FindGameObjectWithTag("RoundSpawner").GetComponent<RoundSpawner>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Tutorial" && Input.GetKeyDown(KeyCode.Space))
        {
            if (roundSpawner.GetRoundFinished())
            {
                PressPlay();
            }
            else if (Time.timeScale == 1f)
            {
                PressFast();
            }
            else if (Time.timeScale == 2f)
            {
                PressPlay();
            }
        }
    }

    public void PressPlay()
    {
        if (Tutorial.canPlay)
        {
            tutorial.ActivatePrompt();
        }

        if (roundSpawner.GetRoundFinished())
        {
            roundSpawner.SpawnEnemies();
        }

        Time.timeScale = 1f;
        SelectButton(play);
        DeselectButton(fast);
    }

    public void PressFast()
    {
        if (roundSpawner.GetRoundFinished())
            return;

        Time.timeScale = 2f;

        DeselectButton(play);
        SelectButton(fast);
    }

    public void ResetButtons()
    {
        DeselectButton(play);
        DeselectButton(fast);
    }

    private void SelectButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = selectedColour;
        button.colors = colors;
    }

    private void DeselectButton(Button button)
    {
        var colors = button.colors;
        colors.normalColor = Color.white;
        button.colors = colors;
    }

}
