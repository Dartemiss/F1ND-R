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
    public List<AudioSource> portalSounds;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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
}
