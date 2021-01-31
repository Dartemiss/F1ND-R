using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class PlayerSelectionManager : MonoBehaviour
{
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

    public enum InputType
    {
        KEYBOARD,
        CONTROLLER,
        NONE
    }

    public GameObject startButton;

    public List<PlayerCardManager> playerCards;
    public Sprite keyboardStartSprite;
    public Sprite controllerStartSprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AreAllPlayhersReady()
    {

    }

    public void ConfirmPlayerSelection()
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
    }

    public PlayerCardManager GetNextAvailablePlayerCard()
    {
        foreach (PlayerCardManager playerCard in playerCards)
        {
            if (!playerCard.IsAssigned())
            {
                return playerCard;
            }
        }

        return null;
    }

    public InputSystemUIInputModule GetStartGameUIInput()
    {
        return startButton.GetComponent<InputSystemUIInputModule>();
    }

    public void SetPlayerOneInputType(InputType inputType)
    {
        if (inputType == InputType.CONTROLLER)
        {
            startButton.GetComponent<Image>().sprite = controllerStartSprite;
        }
        else
        {
            startButton.GetComponent<Image>().sprite = keyboardStartSprite;
        }
        startButton.GetComponent<Image>().SetNativeSize();
    }
}
