using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0

public class Enemy : MonoBehaviour
{
    [Header("Enemy Values")]
    public float startSpeed = 10f;
    [HideInInspector] public float speed;
    [SerializeField] float startHealth = 100f;
    private float health;
    [SerializeField] int value = 50;
    public string enemyType = "Red";
    public int enemyIndex;
    private bool canDie = true;
    private string turretType = null;


    [Header("References")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] Image healthbar;
    [SerializeField] GameObject healthbarBG;
    //private MoneyUI moneyUI;
    private PlayerStatsUI statsUI;



    private void Start()
    {
        healthbarBG.SetActive(false);

        speed = startSpeed;
        health = startHealth;
        //moneyUI = GameObject.FindGameObjectWithTag("MoneyUI").GetComponent<MoneyUI>();
        statsUI = GameObject.FindGameObjectWithTag("StatsUI").GetComponent<PlayerStatsUI>();
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;

        healthbarBG.SetActive(true);

        healthbar.fillAmount = health / startHealth;
        
        if (health <= 0)
        {
            if (canDie)
            {
                canDie = false;
                Die();  
            }
        }
    }

    private void Die()
    {
        //Updating Player Stats
        GameStats.currentMoney += value;
        statsUI.ChangeMoney(value);

        UpdateEnemyData();
        UpdateTurretData();


        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        RoundSpawner.enemiesAlive--;

        //Record death data
        DataRecorder.enemyDeathPositions.Add(transform.position);

        Destroy(gameObject);
    }

    public void Slow(float _amount)
    {
        speed = startSpeed * (1f - _amount);
    }

    public void SetTurretType(string _turret)
    {
        turretType = _turret;
    }

    private void UpdateEnemyData()
    {        
        //Updating Enemy Stats
        if (enemyType == "Light")
            GameStats.lightEnemiesKilled++;
        else if (enemyType == "Standard")
            GameStats.standardEnemiesKilled++;
        else if (enemyType == "Tank")
            GameStats.tankEnemiesKilled++;
        else if (enemyType == "Flying")
            GameStats.flyingEnemiesKilled++;

        GameStats.totalEnemiesKilled++;
    }

    private void UpdateTurretData()
    {
        //MG Turrets
        if (turretType == "MG 1")
            GameStats.mgTurretLvl1Kills++;
        else if (turretType == "MG 2")
            GameStats.mgTurretLvl2Kills++;
        else if (turretType == "MG 3")
            GameStats.mgTurretLvl3Kills++;
        else if (turretType == "MG Burst")
            GameStats.mgTurretBurstKills++;
        else if (turretType == "MG Sniper")
            GameStats.mgTurretSniperKills++;
        //Laser Turrets
        else if (turretType == "Laser 1")
            GameStats.laserTurretLvl1Kills++;
        else if (turretType == "Laser 2")
            GameStats.laserTurretLvl2Kills++;
        else if (turretType == "Laser 3")
            GameStats.laserTurretLvl3Kills++;
        else if (turretType == "Laser AOE")
            GameStats.laserTurretAOEKills++;
        else if (turretType == "Laser Damage")
            GameStats.laserTurretDamageKills++;
        //rocket Turrets
        else if (turretType == "Rocket 1")
            GameStats.rocketTurretLvl1Kills++;
        else if (turretType == "Rocket 2")
            GameStats.rocketTurretLvl2Kills++;
        else if (turretType == "Rocket 3")
            GameStats.rocketTurretLvl3Kills++;
        else if (turretType == "Rocket RF")
            GameStats.rocketTurretRFKills++;
        else if (turretType == "Rocket Nuke")
            GameStats.rocketTurretNukeKills++;
    }

    public void SetValue(int amount)
    {
        value = amount;
    }

    public void SetIndex(int index)
    {
        enemyIndex = index;
    }
}



