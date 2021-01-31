using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelCanvasManager : MonoBehaviour
{
    public static MainLevelCanvasManager instance = null;
    public bool tutorialEnded = false;
    FlamaTimer noInputTimer;

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

    public GameObject HUD;
    public GameObject tutorial;

    // Start is called before the first frame update
    void Start()
    {
        noInputTimer = transform.gameObject.AddComponent<FlamaTimer>();
        noInputTimer.StartTimer(4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHUD()
    {
        if (!noInputTimer.HasTimedOut())
        {
            return;
        }

        tutorial.SetActive(false);
        HUD.SetActive(true);
        tutorialEnded = true;
        LevelSoundManager.instance.PlayPickupSound();
        GameManager.instance.StartGame();
    }
}
