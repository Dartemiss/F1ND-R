using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectionManager : MonoBehaviour
{


    public enum InputType
    {
        KEYBOARD,
        CONTROLLER
    }

    public GameObject canvas;

    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject exitButton;


    public static PlayerSelectionManager instance = null;

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
        if(Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.bButton.wasPressedThisFrame)
        {
            //TODO!
            //Close();
        }
    }

    public void CancelPlayerSelection()
    {
        MainMenuManager.instance.OpenTitleScreen();
    }

    public void ConfirmPlayers()
    {
        if (PlayerConfigurationManager.Instance.AllReadyStartGame()) { GameManager.instance.LoadMainLevel(); }
    }

    public void Close()
    {
        transform.gameObject.SetActive(false);
        PlayerConfigurationManager.Instance.StopPlayerConfiguration();
        canvas.SetActive(true);
        MainMenuManager.instance.OpenTitleScreen();
    }

    public void Open()
    {
        transform.gameObject.SetActive(true);
        PlayerConfigurationManager.Instance.StartPlayerConfiguration();
        canvas.SetActive(false);
        ShowButtons(false);
    }

    public void ShowButtons(bool enableButton)
    {
        startButton.SetActive(enableButton);
        exitButton.SetActive(enableButton);
    }
}
