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

    [SerializeField]
    private MeshRenderer playerMesh;

    public InputAction moveInput;

    private PlayerControls controls;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        controls = new PlayerControls();

    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        playerMesh.material = config.PlayerMaterial;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if(obj.action.name == controls.Gameplay.Movement.name)
        {
            OnMove(obj);
        }

    }

    public void OnMove(CallbackContext ctx)
    {
        if (playerMovement != null)
        {
            playerMovement.MovementDirection(ctx.ReadValue<Vector2>());
        }
    }
}
