using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerProvisional : MonoBehaviour
{
    public static GameManagerProvisional instance = null;

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

    public void CloseGame()
    {
        Application.Quit();
    }
}
