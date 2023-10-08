using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
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

public static class DataRecorder 
{
    public static List<Vector3> enemyDeathPositions = new List<Vector3>();
    public static List<Vector3> turretPostions = new List<Vector3>();

    public static bool RecordEnemyDeathPositions()
    {
        string filePath = GetFilePath("HM_EnemyDeaths");

        bool result = false;

        using (StreamWriter sw = File.AppendText(filePath))
        {
            foreach (Vector3 deathPos in enemyDeathPositions)
            {
                string lineToAdd = deathPos.x + "," + deathPos.y + "," + deathPos.z;

                sw.WriteLine(lineToAdd);
            }
            sw.Close();
        }

        TextAsset asset = Resources.Load<TextAsset>(filePath);
        ////Print the text from the file
        result = true;//If we get to this part of our code, this means things went ok, so we return true. 
        return result;
    }

    public static bool RecordTurretPostions()
    {
        string filePath = GetFilePath("HM_Turrets");

        bool result = false;

        using (StreamWriter sw = File.AppendText(filePath))
        {
            foreach (Vector3 turretPos in turretPostions)
            {
                string lineToAdd = turretPos.x + "," + turretPos.y + "," + turretPos.z;

                sw.WriteLine(lineToAdd);
            }
            sw.Close();
        }

        ////Print the text from the file
        result = true;//If we get to this part of our code, this means things went ok, so we return true. 
        return result;
    }

    public static string GetFilePath(string _fileName)
    {
        string filePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("Username") + "/" + PlayerPrefs.GetString("Username") + "_" + SceneManager.GetActiveScene().name + "_" + _fileName + ".txt";

        return filePath;
    }
}
