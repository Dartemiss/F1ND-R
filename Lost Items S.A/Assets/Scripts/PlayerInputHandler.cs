using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;
    private PlayerMovement playerMovement;
    private PlayerController playerController;

    [SerializeField]
    private Transform playerMesh;

    public InputAction moveInput;

    private PlayerControls controls;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
        controls = new PlayerControls();

    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;


        foreach (Transform thisChild in playerMesh)
        {
            MeshRenderer thisMeshRenderer = thisChild.GetComponent<MeshRenderer>();

            if (thisMeshRenderer)
            {
                thisMeshRenderer.material = config.PlayerMaterial;
            }
        }

        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if(obj.action.name == controls.Gameplay.Movement.name)
        {
            OnMove(obj);
        }

        if(obj.action.name == controls.Gameplay.Interact.name && obj.performed)
        {
            OnInteract();
        }

    }

    public void OnMove(CallbackContext ctx)
    {
        if (playerMovement != null)
        {
            playerMovement.MovementDirection(ctx.ReadValue<Vector2>());
        }
    }

    public void OnInteract()
    {
        if (playerController != null)
        {
            playerController.InteractWithObject();
        }
    }
}
