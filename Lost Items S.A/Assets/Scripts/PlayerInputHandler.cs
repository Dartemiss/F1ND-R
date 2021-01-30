using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    [SerializeField]
    private MeshRenderer playerMesh;

    public InputAction moveInput;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var movers = FindObjectsOfType<PlayerMovement>();
        var index = playerInput.playerIndex;
        playerMovement = movers.FirstOrDefault(pm => pm.GetPlayerIndex() == index);

    }

    public void OnMove(CallbackContext ctx)
    {
        if (playerMovement != null)
        {
            playerMovement.MovementDirection(ctx.ReadValue<Vector2>());
        }
    }
}
