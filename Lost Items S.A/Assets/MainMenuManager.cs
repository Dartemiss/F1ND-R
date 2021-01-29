using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    int hoveredButtonIndex = -1;
    public List<Button> mainMenuButtons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            NavigateDown();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            NavigateUp();
        }
    }

    public void NavigateDown()
    {
        if (hoveredButtonIndex == -1)
        {
            hoveredButtonIndex = 0;
            return;
        }

        hoveredButtonIndex = (hoveredButtonIndex + 1) % mainMenuButtons.Count;
        mainMenuButtons[hoveredButtonIndex].Select();
    }

    public void NavigateUp()
    {
        if (hoveredButtonIndex == -1)
        {
            hoveredButtonIndex = 0;
            return;
        }

        hoveredButtonIndex = hoveredButtonIndex - 1;
        if (hoveredButtonIndex == -1)
        {
            hoveredButtonIndex = mainMenuButtons.Count - 1;
        }
        mainMenuButtons[hoveredButtonIndex].Select();
    }

    public void AcceptButton()
    {
        if (hoveredButtonIndex == -1)
        {
            hoveredButtonIndex = 0;
            return;
        }

        mainMenuButtons[hoveredButtonIndex].onClick.Invoke();
    }
}
