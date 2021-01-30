using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Vector2 movementInput;
    private Vector3 movement;
    NavMeshAgent agent;

    [SerializeField]
    private int playerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        movement.Set(movementInput.x, 0f, movementInput.y);
        agent.Move(movement * Time.deltaTime * agent.speed);
        agent.SetDestination(transform.position + movement);
    }

    public void MovementDirection(Vector2 direction)
    {
        movementInput = direction;
    }

    public int GetPlayerIndex() { return playerIndex; }
}
