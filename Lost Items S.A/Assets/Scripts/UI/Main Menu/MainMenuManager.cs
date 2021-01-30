using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance = null;

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

    public TittleScreenManager tittleScreenManager;
    public PlayerSelectionManager playerSelectionManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPlayerSelectionScreen()
    {
        tittleScreenManager.Close();
        playerSelectionManager.Open();
    }

    public void OpenTitleScreen()
    {
        playerSelectionManager.Close();
        tittleScreenManager.Open();
    }
}
