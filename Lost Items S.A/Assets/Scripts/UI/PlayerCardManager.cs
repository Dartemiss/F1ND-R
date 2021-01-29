using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardManager : MonoBehaviour
{
    public Text requestInputText;
    public Image selectedInput;

    public Sprite keyboardSprite;
    public Sprite controllerSprite;

    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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

        selectedInput.enabled = true;
        requestInputText.enabled = false;
        activated = true;
    }
}
