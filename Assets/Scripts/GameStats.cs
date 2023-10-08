using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a script of my own making to store stats used for data collection.
public class GameStats : MonoBehaviour
{
    [Header("User Stats")]
    public static string username = "PlaceholderUsername";

    [Header("Player Stats")]
    public static int wavesCompleted;
    public static int roundReached;

    public static int healthRemaining;
    public static int healthLost;

    public int startHealth = 20;

    [Header("Level Stats")]
    public static int level01Count, level02Count, level03Count, level04Count, level05Count = 0;

    [Header("Money Stats")]
    public static int currentMoney;
    public static int moneySpent;
    public int startMoney = 400;
    public static int currentUpgrades;
    public static int upgradesSpent;
    public int startUpgrades = 5;

    //MG Turrets
    public static int mgTurretLvl1Built, mgTurretLvl2Built, mgTurretLvl3Built, mgTurretBurstBuilt, mgTurretSniperBuilt;
    public static int mgTurretLvl1Sold, mgTurretLvl2Sold, mgTurretLvl3Sold, mgTurretBurstSold, mgTurretSniperSold;
    public static int mgTurretLvl1Kills, mgTurretLvl2Kills, mgTurretLvl3Kills, mgTurretBurstKills, mgTurretSniperKills;

    //Laser Turrets
    public static int laserTurretLvl1Built, laserTurretLvl2Built, laserTurretLvl3Built, laserTurretAOEBuilt, laserTurretDamageBuilt;
    public static int laserTurretLvl1Sold, laserTurretLvl2Sold, laserTurretLvl3Sold, laserTurretAOESold, laserTurretDamageSold;
    public static int laserTurretLvl1Kills, laserTurretLvl2Kills, laserTurretLvl3Kills, laserTurretAOEKills, laserTurretDamageKills;

    //Rocket Turrets
    public static int rocketTurretLvl1Built, rocketTurretLvl2Built, rocketTurretLvl3Built, rocketTurretRFBuilt, rocketTurretNukeBuilt;
    public static int rocketTurretLvl1Sold, rocketTurretLvl2Sold, rocketTurretLvl3Sold, rocketTurretRFSold, rocketTurretNukeSold;
    public static int rocketTurretLvl1Kills, rocketTurretLvl2Kills, rocketTurretLvl3Kills, rocketTurretRFKills, rocketTurretNukeKills;

    //Enemies
    public static int standardEnemiesKilled, lightEnemiesKilled, tankEnemiesKilled, flyingEnemiesKilled, totalEnemiesKilled;
    public static int standardEnemiesAtEnd, lightEnemiesAtEnd, tankEnemiesAtEnd, flyingEnemiesAtEnd, totalEnemiesAtEnd;

    [Header("Misc. Stats")]
    public static int gameNumber = 1;
    public static bool firstLaunch = true;
    public static bool tutorialCompleted = false;

    private void Start()
    {
        wavesCompleted = 0;
        roundReached = 0;

        //Money
        currentMoney = startMoney;
        currentUpgrades = startUpgrades;

        //Health
        healthRemaining = startHealth;
        healthLost = 0;

        //MG Turrets
        mgTurretLvl1Built = mgTurretLvl2Built = mgTurretLvl3Built = mgTurretBurstBuilt = mgTurretSniperBuilt = 0;
        mgTurretLvl1Sold = mgTurretLvl2Sold = mgTurretLvl3Sold = mgTurretBurstSold = mgTurretSniperSold = 0;
        mgTurretLvl1Kills = mgTurretLvl2Kills = mgTurretLvl3Kills = mgTurretBurstKills = mgTurretSniperKills = 0;

        //Laser Turrets
        laserTurretLvl1Built = laserTurretLvl2Built = laserTurretLvl3Built = laserTurretAOEBuilt = laserTurretDamageBuilt = 0;
        laserTurretLvl1Sold = laserTurretLvl2Sold = laserTurretLvl3Sold = laserTurretAOESold = laserTurretDamageSold = 0;
        laserTurretLvl1Kills = laserTurretLvl2Kills = laserTurretLvl3Kills = laserTurretAOEKills = laserTurretDamageKills = 0;

        //Rocket Turrets
        rocketTurretLvl1Built = rocketTurretLvl2Built = rocketTurretLvl3Built = rocketTurretRFBuilt = rocketTurretNukeBuilt = 0;
        rocketTurretLvl1Sold = rocketTurretLvl2Sold = rocketTurretLvl3Sold = rocketTurretRFSold = rocketTurretNukeSold = 0;
        rocketTurretLvl1Kills = rocketTurretLvl2Kills = rocketTurretLvl3Kills = rocketTurretRFKills = rocketTurretNukeKills = 0;

        //Enemies
        standardEnemiesKilled = lightEnemiesKilled = tankEnemiesKilled = flyingEnemiesKilled = totalEnemiesKilled = 0;
        standardEnemiesAtEnd = lightEnemiesAtEnd = tankEnemiesAtEnd = flyingEnemiesAtEnd = totalEnemiesAtEnd = 0;

}
}
