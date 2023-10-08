using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0

public class PlayerStatsUI : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Text health;

    [Header("Money")]
    [SerializeField] Text money;
    [SerializeField] Text moneyChange;

    [Header("Upgrades")]
    [SerializeField] Text upgrades;
    [SerializeField] Text upgradesChange;

    private void Start()
    {
        moneyChange.enabled = false;
        upgradesChange.enabled = false;
    }

    private void Update()
    {
        UpdateHealth();
        UpdateMoney();
        UpdateUpgrades();
    }

    //Health
    private void UpdateHealth()
    {
        health.text = GameStats.healthRemaining.ToString();
    }

    //Money
    private void UpdateMoney()
    {
        money.text = GameStats.currentMoney.ToString();
    }

    public void ChangeMoney(int change)
    {
        StartCoroutine(UpdateMoneyChange(change));
    }

    IEnumerator UpdateMoneyChange(int change)
    {
        moneyChange.enabled = true;

        if (change <= 0)
        {
            moneyChange.color = Color.red;
            moneyChange.text = "-" + -change;
        }
        else
        {
            moneyChange.color = Color.green;
            moneyChange.text = "+" + change;
        }

        yield return new WaitForSeconds(2f);

        moneyChange.enabled = false;
    }

    //Upgrades
    private void UpdateUpgrades()
    {
        upgrades.text = GameStats.currentUpgrades.ToString();
    }
    public void ChangeUpgrades(int change)
    {
        StartCoroutine(UpdateUpgradesChange(change));
    }

    IEnumerator UpdateUpgradesChange(int change)
    {
        upgradesChange.enabled = true;

        if (change <= 0)
        {
            upgradesChange.color = Color.red;
            upgradesChange.text = "-" + -change;
        }
        else
        {
            upgradesChange.color = Color.green;
            upgradesChange.text = "+" + change;
        }

        yield return new WaitForSeconds(2f);

        upgradesChange.enabled = false;
    }

}
