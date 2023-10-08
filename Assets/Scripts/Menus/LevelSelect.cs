using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


public class LevelSelect : MonoBehaviour
{
    [SerializeField] SceneFader sceneFader;

    [SerializeField] Button[] levelButtons;

    private int levelReached;

    [SerializeField] int maxLevels = 3;


    [SerializeField] GameObject tutorialPrompt;

    [SerializeField] Image tutorialComplete, level1Complete, level2Complete, level3Complete, level4Complete, level5Complete;

    [Header("Level Preview")]
    [SerializeField] Text levelTitle;
    [SerializeField] Image tutorial, level01, level02, level03, level04, level05;

    private void Start()
    {
        if (PlayerPrefs.GetInt(Discord.username + "_LevelReached") <= 0)
        {
            PlayerPrefs.SetInt(Discord.username + "_LevelReached", 1);
        }
        else if (PlayerPrefs.GetInt(Discord.username + "_LevelReached") >= maxLevels)
        {
            PlayerPrefs.SetInt(Discord.username + "_LevelReached", maxLevels);
        }

        levelReached = PlayerPrefs.GetInt(Discord.username + "_LevelReached");

        UpdateButtons();

        Discord.AddToFile("Summary.txt", "LOADING: Level Select");

        if (GameStats.firstLaunch && PlayerPrefs.GetInt(Discord.username + "_TutorialComplete") == 0)
        {
            tutorialPrompt.SetActive(true);
            GameStats.firstLaunch = false;
        }
        else
        {
            tutorialPrompt.SetActive(false);
        }

        HoverButton("Tutorial");
    }

    private void UpdateButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
            else
            {
                levelButtons[i].interactable = true;
            }

            if (levelReached > 1)
                level1Complete.enabled = true;
            else
                level1Complete.enabled = false;

            if (levelReached > 2)
                level2Complete.enabled = true;
            else
                level2Complete.enabled = false;

            if (levelReached > 3)
                level3Complete.enabled = true;
            else
                level3Complete.enabled = false;

            if (levelReached > 4)
                level4Complete.enabled = true;
            else
                level4Complete.enabled = false;

            if (levelReached > 5)
                level5Complete.enabled = true;
            else
                level5Complete.enabled = false;
        }

        if (GameStats.tutorialCompleted)
            tutorialComplete.enabled = true;
        else
            tutorialComplete.enabled = false;
    }

    public void Select(string levelName)
    {
        Discord.AddToFile("Summary.txt", "LEAVING: Level Select (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        MenuMusicManager.nextSceneIsMenu = false;
        sceneFader.LoadScene(levelName);
    }

    public void PressMenu()
    {
        Discord.AddToFile("Summary.txt", "LEAVING: Level Select (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        sceneFader.LoadScene("Menu_Main");
    }

    public void PressTutorialYes()
    {
        Discord.AddToFile("Summary.txt", "LEAVING: Level Select (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        MenuMusicManager.nextSceneIsMenu = false;
        sceneFader.LoadScene("Tutorial");
    }

    public void PressTutorialNo()
    {
        tutorialPrompt.SetActive(false);
    }
    
    public void HoverButton(string levelName)
    {
        if (levelName == "Tutorial")
        {
            levelTitle.text = "Tutorial Preview:";
            tutorial.enabled = true;
            level01.enabled = false;
            level02.enabled = false;
            level03.enabled = false;
            level04.enabled = false;
            level05.enabled = false;
        }
        else if (levelName == "Level 01")
        {
            levelTitle.text = "Level 1 Preview:";
            tutorial.enabled = false;
            level01.enabled = true;
            level02.enabled = false;
            level03.enabled = false;
            level04.enabled = false;
            level05.enabled = false;
        }
        else if (levelName == "Level 02")
        {
            if (level1Complete.enabled)
            {
                levelTitle.text = "Level 2 Preview:";
                tutorial.enabled = false;
                level01.enabled = false;
                level02.enabled = true;
                level03.enabled = false;
                level04.enabled = false;
                level05.enabled = false;
            }
        }
        else if (levelName == "Level 03")
        {
            if (level2Complete.enabled)
            {
                levelTitle.text = "Level 3 Preview:";
                tutorial.enabled = false;
                level01.enabled = false;
                level02.enabled = false;
                level03.enabled = true;
                level04.enabled = false;
                level05.enabled = false;
            }
        }
        else if (levelName == "Level 04")
        {
            if (level3Complete.enabled)
            {
                levelTitle.text = "Level 4 Preview:";
                tutorial.enabled = false;
                level01.enabled = false;
                level02.enabled = false;
                level03.enabled = false;
                level04.enabled = true;
                level05.enabled = false;
            }
        }
        else if (levelName == "Level 05")
        {
            if (level4Complete.enabled)
            {
                levelTitle.text = "Level 5 Preview:";
                tutorial.enabled = false;
                level01.enabled = false;
                level02.enabled = false;
                level03.enabled = false;
                level04.enabled = false;
                level05.enabled = true;
            }
        }
    }
}
