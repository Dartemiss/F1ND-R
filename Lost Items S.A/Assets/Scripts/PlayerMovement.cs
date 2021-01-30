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
    private const float rotationSpeed = 50f;
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
        InstantlyTurn(agent.destination);
    }

    public void MovementDirection(Vector2 direction)
    {
        movementInput = direction;
    }

    private void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if (agent.velocity.sqrMagnitude < Mathf.Epsilon || (destination - transform.position).magnitude < 0.1f) return;

        Quaternion qDir = Quaternion.LookRotation(agent.velocity.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * rotationSpeed);
    }
}
