using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * this Heatmap tool was originally developed by: Garen O'Donnell
 * Date Created: 04/10/2019
 * Modified by: Hadi Mehrpouya
 * Date of modification: 14/10/2020
 * Modified by: Nikola Drousie
 * Date of modification: 07/11/2021
 * Modified by: Liam Rickman
 * Date of modification: 22/11/2021
 */

public class Heatmap : MonoBehaviour
{
    private static List<Vector3> deathPositions = new List<Vector3>();
    private static GameObject deathHeatmapPrefab;

#if UNITY_EDITOR
    public static string GetFilePath(string _fileName)
    {
        //string filePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("Username") + "/" + PlayerPrefs.GetString("Username") + "_" + SceneManager.GetActiveScene().name + "_" + _fileName + ".txt";

        //Insert this if you want to read data from a custom file. Just edit the "username" part E.G importing from discord
        string customUsername = "Heatmap";
        string filePath = Application.persistentDataPath + "/" + customUsername + "/" + customUsername + "_" + SceneManager.GetActiveScene().name + "_" + _fileName + ".txt";

        return filePath;
    }

    //Convert a string into a vector3
    public static Vector3 StringToVector(string _string)
    {
        Vector3 result = new Vector3();
        string[] values = _string.Split(',');

        if (values.Length == 3)
        {
            result.Set(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }

        return result;
    }

    [MenuItem("Heatmap/Hide All")]
    public static void DestroyHeatmapObjects()
    {
        GameObject[] heatmapObjects = GameObject.FindGameObjectsWithTag("Heatmap");
        for (int i = 0; i < heatmapObjects.Length; i++)
        {
            GameObject.DestroyImmediate(heatmapObjects[i]);
        }
    }

    //ENEMY DEATHS
    [MenuItem("Heatmap/Enemy Deaths/Generate")]
    public static void ReadDeathData()
    {
        deathPositions.Clear();
        string filePath = GetFilePath("HM_EnemyDeaths");



        deathHeatmapPrefab = (GameObject)Resources.Load("deathPrefab", typeof(GameObject));

        StreamReader sr = new StreamReader(filePath);
        string deathCoords = "";
        
        while ((deathCoords = sr.ReadLine()) != null)
        {
            deathPositions.Add(StringToVector(deathCoords));
            deathCoords = "";
        }

        sr.Close();

        RenderDeathData();
    }

    public static void RenderDeathData()
    {
        foreach(Vector3 deathPos in deathPositions)
        {
            Instantiate(deathHeatmapPrefab, deathPos, Quaternion.identity);
        }
    }

    ////My own code to reset enemy deaths 
    [MenuItem("Heatmap/Enemy Deaths/Reset Data")]
    public static void ResetEnemyDeathData()
    {
        string filePath = GetFilePath("HM_EnemyDeaths");
        StreamWriter writer = new StreamWriter(filePath);

        writer.Write("");
    }

    //TURRETS
    [MenuItem("Heatmap/Turrets/Generate")]
    public static void ReadTurretData()
    {
        deathPositions.Clear();
        string filePath = GetFilePath("HM_Turrets");

        deathHeatmapPrefab = (GameObject)Resources.Load("turretPrefab", typeof(GameObject));

        StreamReader sr = new StreamReader(filePath);
        string deathCoords = "";

        while ((deathCoords = sr.ReadLine()) != null)
        {
            deathPositions.Add(StringToVector(deathCoords));
            deathCoords = "";
        }

        sr.Close();

        RenderDeathData();
    }

    public static void RenderTurretData()
    {
        foreach (Vector3 deathPos in deathPositions)
        {
            Instantiate(deathHeatmapPrefab, deathPos, Quaternion.identity);
        }
    }

    ////My own code to reset enemy deaths 
    [MenuItem("Heatmap/Turrets/Reset Data")]
    public static void ResetTurretData()
    {
        string filePath = GetFilePath("HM_Turrets");
        StreamWriter writer = new StreamWriter(filePath);

        writer.Write("");
    }




   

#endif
}

