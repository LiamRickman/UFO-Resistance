using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is of my own making and controls the sound effects.

public class SoundManager : MonoBehaviour
{
    public static AudioClip buttonClick, rocketFire, mgFire, laser, noMoney, build;

    [SerializeField] static AudioSource audioSrc;

    private void Start()
    {
        buttonClick = Resources.Load<AudioClip>("ButtonClick");
        rocketFire = Resources.Load<AudioClip>("RocketFire");
        mgFire = Resources.Load<AudioClip>("MGFire");
        laser = Resources.Load<AudioClip>("Laser");
        noMoney = Resources.Load<AudioClip>("NoMoney");
        build = Resources.Load<AudioClip>("Build");

        audioSrc = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
    }

    public static void PlaySound(string clip)
    {
        switch (clip) 
        {
            case "ButtonClick":
                audioSrc.PlayOneShot(buttonClick);
                break;
            case "RocketFire":
                audioSrc.PlayOneShot(rocketFire);
                break;
            case "MGFire":
                audioSrc.PlayOneShot(mgFire);
                break;
            case "Laser":
                audioSrc.PlayOneShot(laser);
                break;
            case "NoMoney":
                audioSrc.PlayOneShot(noMoney);
                break;
            case "Build":
                audioSrc.PlayOneShot(build);
                break;
        }
    }
}
