using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using static UnityEngine.InputSystem.InputAction;


public class SpawnPlayerCardManager : MonoBehaviour
{
    PlayerCardManager playerAssignedCardManager;

    public PlayerCardManager AssignPlayerCard()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();

        playerAssignedCardManager = MainMenuManager.instance.GetNextAvailableCard();

        PlayerSelectionManager.InputType inputType;

        if (playerInput.currentControlScheme == "Keyboard")
        {
            inputType = PlayerSelectionManager.InputType.KEYBOARD;
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            inputType = PlayerSelectionManager.InputType.CONTROLLER;
        }
        else
        {
            inputType = PlayerSelectionManager.InputType.NONE;
            Debug.Log("Incorret input type: " + playerInput.currentControlScheme);
        }
        playerAssignedCardManager.Activate(inputType);

        return playerAssignedCardManager;
    }
}
