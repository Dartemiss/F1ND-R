using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
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
}
