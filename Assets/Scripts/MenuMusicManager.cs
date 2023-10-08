using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a script of my own design for controlling menu music.
public class MenuMusicManager : MonoBehaviour
{
    public static MenuMusicManager instance = null;

    public static bool nextSceneIsMenu = true;

    private void Awake()
    {
        if (instance != null && instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (!nextSceneIsMenu)
        {
            instance.GetComponent<AudioSource>().Pause();
        }
        else
        {
            if (!instance.GetComponent<AudioSource>().isPlaying)
            {
                instance.GetComponent<AudioSource>().PlayDelayed(1);
            }
        }
    }
}
