using UnityEngine;

//Script started out from a YouTube tutorial but has been iterated on as the game was developed.
//Tutorial: https://www.youtube.com/watch?v=beuoNuK2tbk&list=PLPV2KyIb3jR4u5jX8za5iU1cqnQPmbzG0


[System.Serializable]
public class TurretBlueprint
{
    public string name;

    public GameObject level1Prefab;
    public GameObject level2Prefab;
    public GameObject level3Prefab;
    public GameObject special1Prefab;
    public GameObject special2Prefab;

    public int level1Cost;
    public int level2Cost;
    public int level3Cost;
    public int specialCost;

    public int sellCost;

    public float level1Range;
}
