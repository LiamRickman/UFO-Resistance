using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script was made by me for each wave in a round.

[System.Serializable]
public class RoundWave
{    
    [Header("Timings")]
    public float spawnRate;
    public float startDelay;

    [Header("Enemy Amounts")]
    public int standardAmount;
    public int lightAmount;
    public int tankAmount;
    public int flyingAmount;
}
