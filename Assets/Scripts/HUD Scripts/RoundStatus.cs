using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is of my own design to control the round controls.
public class RoundStatus : MonoBehaviour
{
    [Header("General")]
    private RoundSpawner roundSpawner;

    [Header("Round Count")]
    [SerializeField] Text rounds;

    [Header("Round State")]
    [SerializeField] GameObject play;
    [SerializeField] GameObject stop;

    private void Start()
    {
        roundSpawner = GameObject.FindGameObjectWithTag("RoundSpawner").GetComponent<RoundSpawner>();   
    }

    private void Update()
    {
        UpdateRoundText();
    }

    private void UpdateRoundText()
    {
        if (roundSpawner.GetRoundFinished())
        {
            //Update Round Text
            int roundIndex = roundSpawner.GetRoundIndex();
            roundIndex++;
            rounds.text = "ROUND: " + roundIndex;

            //Update Round State (Round Finished)
            stop.SetActive(true);
            play.SetActive(false);
        }
        else
        {
            //Update Round State (Round Running)
            stop.SetActive(false);
            play.SetActive(true);
        }
    }
}
