using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = this.gameObject.GetComponent<Button>();

        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        SoundManager.PlaySound("ButtonClick");
    }

    
}
