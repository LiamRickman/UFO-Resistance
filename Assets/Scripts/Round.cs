using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script contains values of the next round of enemies.

[System.Serializable]
public class Round
{
    public string name;
    public int enemyValue = 10;
    public int upgrades;
    public RoundWave[] waves;

}
