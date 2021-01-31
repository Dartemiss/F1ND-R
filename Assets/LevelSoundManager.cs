using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    public static LevelSoundManager instance = null;

    public AudioSource pickupSound;
    public AudioSource dropSound;
    public AudioSource correctSound;
    public AudioSource wrongSound;
    public AudioSource alarmSound;
    public List<AudioSource> portalSounds;

    bool alarmPlayed = false;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void PlayCorrectSound()
    {
        correctSound.Play();
    }

    public void PlayWrongSound()
    {
        wrongSound.Play();
    }

    public void PlayPickupSound()
    {
        pickupSound.Play();
    }

    public void PlayDropSound()
    {
        dropSound.Play();
    }

    public void PlaySpawnFromPortal()
    {
        int i = (int)Random.Range(0,3);
        portalSounds[i].Play();
    }

    public void PlayAlarmSouns()
    {
        if(!alarmPlayed)
        {
            alarmPlayed = true;
            alarmSound.Play();
        }
    }
}
