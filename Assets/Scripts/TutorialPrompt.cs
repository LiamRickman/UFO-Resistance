using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script is my own and controls the prompts in the tutorial

public class TutorialPrompt : MonoBehaviour
{
    [SerializeField] float delay = 0.1f;
    private string fullText;
    private string currentText = "";
    [SerializeField] Text textBox;
    public Vector3 newCamPos;
    private bool textFinished = false;
    [SerializeField] GameObject image;

    private void Awake()
    {
        fullText = textBox.text;
        if (image != null)
        {
            image.SetActive(false);
        }

    }

    private void Update()
    {
        if (Input.anyKeyDown && !(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
            {
            if (textBox.text != fullText)
            {
                textFinished = true;

                textBox.text = fullText;
            }
        }
    }

    public void OnEnable()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        textBox.text = "";

        for (int i = 0; i < fullText.Length + 1; i++)
        {
            if (!textFinished)
            {
                currentText = fullText.Substring(0, i);
                textBox.text = currentText;
                yield return new WaitForSeconds(delay);
            }
        }
        textFinished = true;
        if (image != null)
        {
            image.SetActive(true);
        }
    }

    public bool GetTextFinished()
    {
        return textFinished;
    }

}
