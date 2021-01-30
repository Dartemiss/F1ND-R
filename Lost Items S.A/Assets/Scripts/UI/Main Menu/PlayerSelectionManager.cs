using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour
{
    public enum InputType
    {
        KEYBOARD,
        CONTROLLER
    }

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CancelPlayerSelection()
    {
        MainMenuManager.instance.OpenTitleScreen();
    }

    public void ConfirmPlayers()
    {
        GameManager.instance.LoadMainLevel();
    }

    public void Close()
    {
        transform.gameObject.SetActive(false);
    }

    public void Open()
    {
        transform.gameObject.SetActive(true);
        PlayerConfigurationManager.Instance.StartPlayerConfiguration();
        canvas.SetActive(false);
    }
}
