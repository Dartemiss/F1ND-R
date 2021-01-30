using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;

    [SerializeField]
    private MeshRenderer playerMesh;

    public InputAction moveInput;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void OnMove(CallbackContext ctx)
    {
        Debug.Log("MOVIIIIIIIIIING");
        playerMovement.MovementDirection(ctx.ReadValue<Vector2>());
    }
}
