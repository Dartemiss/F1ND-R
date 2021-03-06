using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerCardManager : MonoBehaviour
{
    public Image requestInputText;
    public Image selectedInput;

    public Sprite keyboardSprite;
    public Sprite controllerSprite;

    public List<Sprite> finderSprites;
    public Image finderIDSprite;

    public PlayerColor assignedColor;

    public enum PlayerColor
    {
        RED,
        BLUE,
        YELLOW,
        PURPLE,
    }

    bool assigned = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsAssigned()
    {
        return assigned;
    }

    public void Activate(PlayerSelectionManager.InputType inputType)
    {
        switch (inputType)
        {
            case PlayerSelectionManager.InputType.CONTROLLER:
                selectedInput.sprite = controllerSprite;
                break;

            case PlayerSelectionManager.InputType.KEYBOARD:
                selectedInput.sprite = keyboardSprite;
                break;
        }
        selectedInput.SetNativeSize();
        selectedInput.enabled = true;
        requestInputText.enabled = false;
        assigned = true;
        SetFinderIDSprite();

    }

    void SetFinderIDSprite()
    {
        assignedColor = MainMenuManager.instance.GetNextAvailableColor();
        finderIDSprite.sprite = finderSprites[(int)assignedColor];
    }
}
