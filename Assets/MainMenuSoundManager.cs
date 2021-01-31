using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    public static MainMenuSoundManager instance = null;

    public AudioSource playerJoinSound;
    public AudioSource pressStartSound;

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

    public void PlayPlayerJoinSound()
    {
        playerJoinSound.Play();
    }

    public void PlayPressStartSound()
    {
        pressStartSound.Play();
    }
}