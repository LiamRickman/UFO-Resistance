using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This is a script of my own making.

public class GameController : MonoBehaviour
{
    public static bool gameOver = false;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject gameWonUI;
    private GameManager gm;

    private float levelTimer;

    private string sceneName;

    private void Start()
    {
        GameStats.moneySpent = 0;
        GameStats.roundReached = 0;

        if (GameObject.FindGameObjectsWithTag("GameManager").Length != 0)
        {
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.UpdateVolume();
        }

        Tutorial.nodeUIDisabled = false;


        sceneName = SceneManager.GetActiveScene().name.ToString();
        gameOver = false;

#if UNITY_EDITOR
        if (Discord.username == null)
        {
            Discord.username = "Default";
        }
#endif
        Discord.AddToFile("Summary.txt", "LOADING: " + sceneName);

        ResetDataRecorder();

        if (!Discord.FileExists(sceneName + ".csv"))
        {
            Discord.AddToFile(sceneName + ".csv", SceneManager.GetActiveScene().name + "," + Discord.username, false);
            Discord.AddToFile(sceneName + ".csv", "", false);
        }
    }
    
    private void Update()
    {
        //Keep track of time in level
        levelTimer = Time.timeSinceLevelLoad;

        if (gameOver)
            return;

        if (GameStats.healthRemaining <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        UpdateDataRecorder();
        UpdateDiscord();

        Time.timeScale = 1f;

        gameOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        UpdateDataRecorder();
        UpdateDiscord();

        Time.timeScale = 1f;
        int levelReached = PlayerPrefs.GetInt(Discord.username + "_LevelReached") + 1;
        PlayerPrefs.SetInt(Discord.username + "_LevelReached", levelReached);

        gameOver = true;
        gameWonUI.SetActive(true);
    }

    public void UpdateDiscord()
    {
        //Set Play Header
        if (SceneManager.GetActiveScene().name == "Level 01")
        {
            GameStats.level01Count++;
            Discord.AddToFile(sceneName + ".csv", "Play #" + GameStats.level01Count + ", Round Reached: " + GameStats.roundReached, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 02")
        {
            GameStats.level02Count++;
            Discord.AddToFile(sceneName + ".csv", "Play #" + GameStats.level02Count + ", Round Reached: " + GameStats.roundReached, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 03")
        {
            GameStats.level03Count++;
            Discord.AddToFile(sceneName + ".csv", "Play #" + GameStats.level03Count + ", Round Reached: " + GameStats.roundReached, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 04")
        {
            GameStats.level04Count++;
            Discord.AddToFile(sceneName + ".csv", "Play #" + GameStats.level04Count + ", Round Reached: " + GameStats.roundReached, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 05")
        {
            GameStats.level05Count++;
            Discord.AddToFile(sceneName + ".csv", "Play #" + GameStats.level05Count + ", Round Reached: " + GameStats.roundReached, false);
        }
        Discord.AddToFile(sceneName + ".csv", "", false);

        //Health Stats
        Discord.AddToFile(sceneName + ".csv", "Health Stats, Remaining, Lost", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.healthRemaining + "," + GameStats.healthLost, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        //Money Stats
        Discord.AddToFile(sceneName + ".csv", "Currency Stats, Current Money, Money Spent, Current Upgrades, Upgrades Spent", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.currentMoney + "," + GameStats.moneySpent + "," + GameStats.currentUpgrades + "," + GameStats.upgradesSpent, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        //Enemy Stats
        Discord.AddToFile(sceneName + ".csv", "Enemies Killed, Standard, Light, Tank, Flying", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.standardEnemiesKilled + "," + GameStats.lightEnemiesKilled + "," + GameStats.tankEnemiesKilled + "," + GameStats.flyingEnemiesKilled, false);
        Discord.AddToFile(sceneName + ".csv", "Enemies At End, Standard, Light, Tank, Flying", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.standardEnemiesAtEnd + "," + GameStats.lightEnemiesAtEnd + "," + GameStats.tankEnemiesAtEnd + "," + GameStats.flyingEnemiesAtEnd, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        //MG Stats
        Discord.AddToFile(sceneName + ".csv", "MG Built, MG Level 1, MG Level 2, MG Level 3, MG Burst, MG Sniper", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.mgTurretLvl1Built + "," + GameStats.mgTurretLvl2Built + "," + GameStats.mgTurretLvl3Built + "," + GameStats.mgTurretBurstBuilt + "," + GameStats.mgTurretSniperBuilt, false);
        Discord.AddToFile(sceneName + ".csv", "MG Sold, MG Level 1, MG Level 2, MG Level 3, MG Burst, MG Sniper", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.mgTurretLvl1Sold + "," + GameStats.mgTurretLvl2Sold + "," + GameStats.mgTurretLvl3Sold + "," + GameStats.mgTurretBurstSold + "," + GameStats.mgTurretSniperSold, false);
        Discord.AddToFile(sceneName + ".csv", "MG Kills, MG Level 1, MG Level 2, MG Level 3, MG Burst, MG Sniper", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.mgTurretLvl1Kills + "," + GameStats.mgTurretLvl2Kills + "," + GameStats.mgTurretLvl3Kills + "," + GameStats.mgTurretBurstKills + "," + GameStats.mgTurretSniperKills, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        //Laser Stats
        Discord.AddToFile(sceneName + ".csv", "Laser Built, Laser Level 1, Laser Level 2, Laser Level 3, Laser AOE, Laser Damage", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.laserTurretLvl1Built + "," + GameStats.laserTurretLvl2Built + "," + GameStats.laserTurretLvl3Built + "," + GameStats.laserTurretAOEBuilt + "," + GameStats.laserTurretDamageBuilt, false);
        Discord.AddToFile(sceneName + ".csv", "Laser Sold, Laser Level 1, Laser Level 2, Laser Level 3, Laser AOE, Laser Damage", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.laserTurretLvl1Sold + "," + GameStats.laserTurretLvl2Sold + "," + GameStats.laserTurretLvl3Sold + "," + GameStats.laserTurretAOESold + "," + GameStats.laserTurretDamageSold, false);
        Discord.AddToFile(sceneName + ".csv", "Laser Kills, Laser Level 1, Laser Level 2, Laser Level 3, Laser AOE, Laser Damage", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.laserTurretLvl1Kills + "," + GameStats.laserTurretLvl2Kills + "," + GameStats.laserTurretLvl3Kills + "," + GameStats.laserTurretAOEKills + "," + GameStats.laserTurretDamageKills, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        //Rocket Stats
        Discord.AddToFile(sceneName + ".csv", "Rocket Built, Rocket Level 1, Rocket Level 2, Rocket Level 3, Rocket RF, Rocket Nuke", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.rocketTurretLvl1Built + "," + GameStats.rocketTurretLvl2Built + "," + GameStats.rocketTurretLvl3Built + "," + GameStats.rocketTurretRFBuilt + "," + GameStats.rocketTurretNukeBuilt, false);
        Discord.AddToFile(sceneName + ".csv", "Rocket Sold, Rocket Level 1, Rocket Level 2, Rocket Level 3, Rocket RF, Rocket Nuke", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.rocketTurretLvl1Sold + "," + GameStats.rocketTurretLvl2Sold + "," + GameStats.rocketTurretLvl3Sold + "," + GameStats.rocketTurretRFSold + "," + GameStats.rocketTurretNukeSold, false);
        Discord.AddToFile(sceneName + ".csv", "Rocket Kills, Rocket Level 1, Rocket Level 2, Rocket Level 3, Rocket RF, Rocket Nuke", false);
        Discord.AddToFile(sceneName + ".csv", Discord.username + "," + GameStats.rocketTurretLvl1Kills + "," + GameStats.rocketTurretLvl2Kills + "," + GameStats.rocketTurretLvl3Kills + "," + GameStats.rocketTurretRFKills + "," + GameStats.rocketTurretNukeKills, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        Discord.AddToFile(sceneName + ".csv", "", false);

        /*
        Discord.AddToFile(sceneName + ".csv", "", false);

        if (SceneManager.GetActiveScene().name == "Level 01")
        {
            GameStats.level01Count++;
            Discord.AddToFile(sceneName + ".csv", "Game #" + GameStats.level01Count, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 02")
        {
            GameStats.level02Count++;
            Discord.AddToFile(sceneName + ".csv", "Game #" + GameStats.level02Count, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 03")
        {
            GameStats.level03Count++;
            Discord.AddToFile(sceneName + ".csv", "Game #" + GameStats.level03Count, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 04")
        {
            GameStats.level04Count++;
            Discord.AddToFile(sceneName + ".csv", "Game #" + GameStats.level04Count, false);
        }
        else if (SceneManager.GetActiveScene().name == "Level 05")
        {
            GameStats.level05Count++;
            Discord.AddToFile(sceneName + ".csv", "Game #" + GameStats.level05Count, false);
        }
        Discord.AddToFile(sceneName + ".csv", "", false);

        Discord.AddToFile(sceneName + ".csv", "Health Stats", false);
        Discord.AddToFile(sceneName + ".csv", ", Remaining, Lost", false);
        Discord.AddToFile(sceneName + ".csv", "," + GameStats.healthRemaining + "," + GameStats.healthLost, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        Discord.AddToFile(sceneName + ".csv", "Enemy Stats", false);
        Discord.AddToFile(sceneName + ".csv", ", Killed, Hit End", false);
        Discord.AddToFile(sceneName + ".csv", "Standard," + GameStats.standardEnemiesKilled + "," + GameStats.standardEnemiesAtEnd, false);
        Discord.AddToFile(sceneName + ".csv", "Light," + GameStats.lightEnemiesKilled + "," + GameStats.lightEnemiesAtEnd, false);
        Discord.AddToFile(sceneName + ".csv", "Tank," + GameStats.tankEnemiesKilled + "," + GameStats.tankEnemiesAtEnd, false);
        Discord.AddToFile(sceneName + ".csv", "Flying," + GameStats.flyingEnemiesKilled + "," + GameStats.flyingEnemiesAtEnd, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        Discord.AddToFile(sceneName + ".csv", "MG Turrets", false);
        Discord.AddToFile(sceneName + ".csv", ", Built, Sold, Kills", false);
        Discord.AddToFile(sceneName + ".csv", "MG Level 1," + GameStats.mgTurretLvl1Built + "," + GameStats.mgTurretLvl1Sold + "," + GameStats.mgTurretLvl1Kills, false);
        Discord.AddToFile(sceneName + ".csv", "MG Level 2," + GameStats.mgTurretLvl2Built + "," + GameStats.mgTurretLvl2Sold + "," + GameStats.mgTurretLvl2Kills, false);
        Discord.AddToFile(sceneName + ".csv", "MG Level 3," + GameStats.mgTurretLvl3Built + "," + GameStats.mgTurretLvl3Sold + "," + GameStats.mgTurretLvl3Kills, false);
        Discord.AddToFile(sceneName + ".csv", "MG Burst," + GameStats.mgTurretBurstBuilt + "," + GameStats.mgTurretBurstSold + "," + GameStats.mgTurretBurstKills, false);
        Discord.AddToFile(sceneName + ".csv", "MG Sniper," + GameStats.mgTurretSniperBuilt + "," + GameStats.mgTurretSniperSold + "," + GameStats.mgTurretSniperKills, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        Discord.AddToFile(sceneName + ".csv", "Laser Turrets", false);
        Discord.AddToFile(sceneName + ".csv", ", Built, Sold, Kills", false);
        Discord.AddToFile(sceneName + ".csv", "Laser Level 1," + GameStats.laserTurretLvl1Built + "," + GameStats.laserTurretLvl1Sold + "," + GameStats.laserTurretLvl1Kills, false);
        Discord.AddToFile(sceneName + ".csv", "Laser Level 2," + GameStats.laserTurretLvl2Built + "," + GameStats.laserTurretLvl2Sold + "," + GameStats.laserTurretLvl2Kills, false);
        Discord.AddToFile(sceneName + ".csv", "Laser Level 3," + GameStats.laserTurretLvl3Built + "," + GameStats.laserTurretLvl3Sold + "," + GameStats.laserTurretLvl3Kills, false);
        Discord.AddToFile(sceneName + ".csv", "Laser AOE Slow," + GameStats.laserTurretAOEBuilt + "," + GameStats.laserTurretAOESold + "," + GameStats.laserTurretAOEKills, false);
        Discord.AddToFile(sceneName + ".csv", "Laser Damage," + GameStats.laserTurretDamageBuilt + "," + GameStats.laserTurretDamageSold + "," + GameStats.laserTurretDamageKills, false);
        Discord.AddToFile(sceneName + ".csv", "", false);

        Discord.AddToFile(sceneName + ".csv", "Rocket Turrets", false);
        Discord.AddToFile(sceneName + ".csv", ", Built, Sold, Kills", false);
        Discord.AddToFile(sceneName + ".csv", "Rocket Level 1," + GameStats.rocketTurretLvl1Built + "," + GameStats.rocketTurretLvl1Sold + "," + GameStats.rocketTurretLvl1Kills, false);
        Discord.AddToFile(sceneName + ".csv", "Rocket Level 2," + GameStats.rocketTurretLvl2Built + "," + GameStats.rocketTurretLvl2Sold + "," + GameStats.rocketTurretLvl2Kills, false);
        Discord.AddToFile(sceneName + ".csv", "Rocket Level 3," + GameStats.rocketTurretLvl3Built + "," + GameStats.rocketTurretLvl3Sold + "," + GameStats.rocketTurretLvl3Kills, false);
        Discord.AddToFile(sceneName + ".csv", "Rocket Rapid-Fire," + GameStats.rocketTurretRFBuilt + "," + GameStats.rocketTurretRFSold + "," + GameStats.rocketTurretRFKills, false);
        Discord.AddToFile(sceneName + ".csv", "Rocket Nuke," + GameStats.rocketTurretNukeBuilt + "," + GameStats.rocketTurretNukeSold + "," + GameStats.rocketTurretNukeKills, false);
        Discord.AddToFile(sceneName + ".csv", "", false);
        */



        ////Text File
        //DiscordWebhook.AddToFile(sceneName, "", false);

        //if (SceneManager.GetActiveScene().name == "Level 01")
        //{
        //    GameStats.level01Count++;
        //    DiscordWebhook.AddToFile(sceneName, "Game #" + GameStats.level01Count, false);
        //}
        //else if (SceneManager.GetActiveScene().name == "Level 02")
        //{
        //    GameStats.level02Count++;
        //    DiscordWebhook.AddToFile(sceneName, "Game #" + GameStats.level02Count, false);
        //}
        //else if (SceneManager.GetActiveScene().name == "Level 03")
        //{
        //    GameStats.level03Count++;
        //    DiscordWebhook.AddToFile(sceneName, "Game #" + GameStats.level03Count, false);
        //}
        //else if (SceneManager.GetActiveScene().name == "Level 04")
        //{
        //    GameStats.level04Count++;
        //    DiscordWebhook.AddToFile(sceneName, "Game #" + GameStats.level04Count, false);
        //}
        //else if (SceneManager.GetActiveScene().name == "Level 05")
        //{
        //    GameStats.level05Count++;
        //    DiscordWebhook.AddToFile(sceneName, "Game #" + GameStats.level05Count, false);
        //}
        //DiscordWebhook.AddToFile(sceneName, "", false);

        //DiscordWebhook.AddToFile(sceneName, "Health Stats", false);
        //DiscordWebhook.AddToFile(sceneName, ", Remaining, Lost", false);
        //DiscordWebhook.AddToFile(sceneName, "," + GameStats.healthRemaining + "," + GameStats.healthLost, false);
        //DiscordWebhook.AddToFile(sceneName, "", false);

        //DiscordWebhook.AddToFile(sceneName, "Enemy Stats", false);
        //DiscordWebhook.AddToFile(sceneName, ", Killed, Hit End", false);
        //DiscordWebhook.AddToFile(sceneName, "Standard," + GameStats.standardEnemiesKilled + "," + GameStats.standardEnemiesAtEnd, false);
        //DiscordWebhook.AddToFile(sceneName, "Light," + GameStats.lightEnemiesKilled + "," + GameStats.lightEnemiesAtEnd, false);
        //DiscordWebhook.AddToFile(sceneName, "Tank," + GameStats.tankEnemiesKilled + "," + GameStats.tankEnemiesAtEnd, false);
        //DiscordWebhook.AddToFile(sceneName, "Flying," + GameStats.flyingEnemiesKilled + "," + GameStats.flyingEnemiesAtEnd, false);
        //DiscordWebhook.AddToFile(sceneName, "", false);

        //DiscordWebhook.AddToFile(sceneName, "MG Turrets", false);
        //DiscordWebhook.AddToFile(sceneName, ", Built, Sold, Kills", false);
        //DiscordWebhook.AddToFile(sceneName, "MG Level 1," + GameStats.mgTurretLvl1Built + "," + GameStats.mgTurretLvl1Sold + "," + GameStats.mgTurretLvl1Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "MG Level 2," + GameStats.mgTurretLvl2Built + "," + GameStats.mgTurretLvl2Sold + "," + GameStats.mgTurretLvl2Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "MG Level 3," + GameStats.mgTurretLvl3Built + "," + GameStats.mgTurretLvl3Sold + "," + GameStats.mgTurretLvl3Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "MG Burst," + GameStats.mgTurretBurstBuilt + "," + GameStats.mgTurretBurstSold + "," + GameStats.mgTurretBurstKills, false);
        //DiscordWebhook.AddToFile(sceneName, "MG Sniper," + GameStats.mgTurretSniperBuilt + "," + GameStats.mgTurretSniperSold + "," + GameStats.mgTurretSniperKills, false);
        //DiscordWebhook.AddToFile(sceneName, "", false);

        //DiscordWebhook.AddToFile(sceneName, "Laser Turrets", false);
        //DiscordWebhook.AddToFile(sceneName, ", Built, Sold, Kills", false);
        //DiscordWebhook.AddToFile(sceneName, "Laser Level 1," + GameStats.laserTurretLvl1Built + "," + GameStats.laserTurretLvl1Sold + "," + GameStats.laserTurretLvl1Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "Laser Level 2," + GameStats.laserTurretLvl2Built + "," + GameStats.laserTurretLvl2Sold + "," + GameStats.laserTurretLvl2Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "Laser Level 3," + GameStats.laserTurretLvl3Built + "," + GameStats.laserTurretLvl3Sold + "," + GameStats.laserTurretLvl3Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "Laser AOE Slow," + GameStats.laserTurretAOEBuilt + "," + GameStats.laserTurretAOESold + "," + GameStats.laserTurretAOEKills, false);
        //DiscordWebhook.AddToFile(sceneName, "Laser Damage," + GameStats.laserTurretDamageBuilt + "," + GameStats.laserTurretDamageSold + "," + GameStats.laserTurretDamageKills, false);
        //DiscordWebhook.AddToFile(sceneName, "", false);

        //DiscordWebhook.AddToFile(sceneName, "Rocket Turrets", false);
        //DiscordWebhook.AddToFile(sceneName, ", Built, Sold, Kills", false);
        //DiscordWebhook.AddToFile(sceneName, "Rocket Level 1," + GameStats.rocketTurretLvl1Built + "," + GameStats.rocketTurretLvl1Sold + "," + GameStats.rocketTurretLvl1Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "Rocket Level 2," + GameStats.rocketTurretLvl2Built + "," + GameStats.rocketTurretLvl2Sold + "," + GameStats.rocketTurretLvl2Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "Rocket Level 3," + GameStats.rocketTurretLvl3Built + "," + GameStats.rocketTurretLvl3Sold + "," + GameStats.rocketTurretLvl3Kills, false);
        //DiscordWebhook.AddToFile(sceneName, "Rocket Rapid-Fire," + GameStats.rocketTurretRFBuilt + "," + GameStats.rocketTurretRFSold + "," + GameStats.rocketTurretRFKills, false);
        //DiscordWebhook.AddToFile(sceneName, "Rocket Nuke," + GameStats.rocketTurretNukeBuilt + "," + GameStats.rocketTurretNukeSold + "," + GameStats.rocketTurretNukeKills, false);
        //DiscordWebhook.AddToFile(sceneName, "", false);

        if (gameWonUI.activeSelf)
        {
            Discord.AddToFile("Summary.txt", "COMPLETED: " + SceneManager.GetActiveScene().name.ToString() + " (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        }
        else if (gameOverUI.activeSelf)
        {
            Discord.AddToFile("Summary.tx", "FAILED: " + SceneManager.GetActiveScene().name.ToString() + " (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
        }

        Discord.AddToFile("Summary.txt", "LEAVING: " + SceneManager.GetActiveScene().name.ToString() + " (Time Spent: " + Time.timeSinceLevelLoad.ToString("F2") + ")");
    }

    public void ResetDataRecorder()
    {
        DataRecorder.enemyDeathPositions = new List<Vector3>();
        DataRecorder.turretPostions = new List<Vector3>();
    }

    public void UpdateDataRecorder()
    {
        DataRecorder.RecordEnemyDeathPositions();
        DataRecorder.RecordTurretPostions();
    }

}
