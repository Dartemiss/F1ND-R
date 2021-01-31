using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

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

    public Stack<PlayerCardManager.PlayerColor> colorsRandomBag;

    // Start is called before the first frame update
    void Start()
    {
        List<PlayerCardManager.PlayerColor> colorRandomArray = new List<PlayerCardManager.PlayerColor>();
        colorRandomArray.Add(PlayerCardManager.PlayerColor.RED);
        colorRandomArray.Add(PlayerCardManager.PlayerColor.BLUE);
        colorRandomArray.Add(PlayerCardManager.PlayerColor.YELLOW);
        colorRandomArray.Add(PlayerCardManager.PlayerColor.PURPLE);
        
        for (int i = colorRandomArray.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            PlayerCardManager.PlayerColor tmp = colorRandomArray[i];
            colorRandomArray[i] = colorRandomArray[swapIndex];
            colorRandomArray[swapIndex] = tmp;
        }

        colorsRandomBag = new Stack<PlayerCardManager.PlayerColor>(colorRandomArray);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPlayerSelectionScreen()
    {
        tittleScreenManager.Close();
        playerSelectionManager.Open();
        MainMenuSoundManager.instance.PlayPressStartSound();
    }
    
    public PlayerCardManager GetNextAvailableCard()
    {
        return playerSelectionManager.GetNextAvailablePlayerCard();
    }

    public void SetPlayerOneInputType(PlayerSelectionManager.InputType inputType)
    {
        playerSelectionManager.SetPlayerOneInputType(inputType);
    }

    public InputSystemUIInputModule GetStartGameUIInput()
    {
        return playerSelectionManager.GetStartGameUIInput();
    }

    public PlayerCardManager.PlayerColor GetNextAvailableColor()
    {
        return colorsRandomBag.Pop();
    }
}
