using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

//This script is used for all my data collection
//It works by creating text and csv files.
//On game close it will add them all to a form and send it to my webhook.
//My webhook is hosted in a discord server.
//This means I dont have to rely on people sending me data. 
//It is also faster than Unity Analytics, letting me analyse data sooner.

public class Discord : MonoBehaviour
{
#if UNITY_EDITOR
    private static string webhookURL = "https://discord.com/api/webhooks/917107638553174076/j1HJYPQ4HORWqmxgQ9VRuCxEDhz_60zRLf5RFX2ga2MdHrNTBMJ_rMndRC27MoKpqym1";
#elif !UNITY_EDITOR
    private static string webhookURL = "https://discord.com/api/webhooks/923995959363534891/WcH9HdabYBs_vM3TP-bsAFZBkYIDFi86jJP5eI3BNo128MA5xhHHcI27ctq43DQY9blN";
#endif

    public static string username;

    private static int fileIndex = 0;

    private static HttpClient httpClient = new HttpClient();
    private static MultipartFormDataContent form = new MultipartFormDataContent();

    //Set Username
    public static void SetUsername(string _username)
    {
        PlayerPrefs.SetString("Username", _username);
        username = PlayerPrefs.GetString("Username");
    }

    //FILE METHODS
    public static string GetFilePath(string _fileName)
    {
        string playCount = PlayerPrefs.GetString("PlayCount").ToString();

        if (!Directory.Exists(Application.persistentDataPath + "/" + username))
        {
            var folder = Directory.CreateDirectory(Application.persistentDataPath + "/" + username);

            string filePath = folder + _fileName;
            return filePath;
        }
        else
        {
            string filePath = Application.persistentDataPath + "/" + username + "/" + username + "_" + _fileName;
            return filePath;
        }
    }

    public static bool FileExists(string _fileName)
    {
        string filePath = GetFilePath(_fileName);

        if (File.Exists(filePath))
            return true;
        else
            return false;
    }

    public static bool FolderExists(string _folderName)
    {
        if (Directory.Exists(Application.persistentDataPath + "/" + _folderName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ClearFile(string _fileName)
    {
        string filePath = GetFilePath(_fileName);

        //Clear the old text file
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public static void AddToFile(string _fileName, string content, bool isDated = true)
    {
        string filePath = GetFilePath(_fileName);

        if (isDated)
            content = "[" + System.DateTime.Now.ToString() + "] " + content;

        using (StreamWriter sw = File.AppendText(filePath))
        {
            sw.WriteLine(content);
            sw.Close();
        }
    }

    public static void EditFile(string _fileName, string content, int lineNumber, bool isDated = true)
    {
        string filePath = GetFilePath(_fileName);

        if (isDated)
            content = "[" + System.DateTime.Now.ToString() + "] " + content;

        var lines = File.ReadAllLines(filePath);
        lines[lineNumber] = content;
        File.WriteAllLines(filePath, lines);
    }


    public static void SendFile(string _fileName)
    {
        string filePath = GetFilePath(_fileName);

        string newFileName = username + "_" + _fileName;

        if (!File.Exists(filePath))
        {
            Debug.Log("No File Found at: " + filePath);
            return;
        }

        Debug.Log("Sending " + newFileName + " to Discord (" + filePath + ")");

        //Sends the text file to discord
        //Adapted from: https://github.com/Not-Cyrus/discord-file-webhook-upload/blob/main/webhook.cs
        HttpClient httpClient = new HttpClient();

        var form = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));

        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

        form.Add(fileContent, "file", newFileName);
        //form.Add(new StringContent(username + " (" + PlayerPrefs.GetString("unity.cloud_userid_h2665564582") + ")"), "username");
        form.Add(new StringContent(username + " (Game #" + PlayerPrefs.GetInt(username + "_SessionID") + ")"), "username");

        httpClient.PostAsync(webhookURL, form);
        return;
    }

    public static void AddFileToForm(string _fileName)
    {
        if (_fileName != "")
        {
            string filePath = GetFilePath(_fileName);

            if (FileExists(_fileName))
            {
                string newFileName = username + "_" + _fileName;
                string tempFileName = "file" + fileIndex;

                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                form.Add(fileContent, tempFileName, newFileName);

                fileIndex++;
            }
            else
            {
                Debug.Log("No File Found At: " + filePath);
            }
        }
    }

    public static void SendFormToDiscord()
    {
        //Webhook Name
        form.Add(new StringContent(username + " (Game #" + PlayerPrefs.GetInt(username + "_SessionID") + ")"), "username");

        //Send to discord
        httpClient.PostAsync(webhookURL, form);

        form = new MultipartFormDataContent();
    }
}
