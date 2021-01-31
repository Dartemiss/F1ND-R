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

    [SerializeField]
    private const float speed = 4f;

    public float currentSpeed;
    private Animator animator;

    Vector3 lastPosition;

    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        movement.Set(movementInput.x, 0f, movementInput.y);
        characterController.Move(movement * Time.deltaTime * speed);
        if(movement != Vector3.zero)
        {
            transform.forward = movement;
        }
        characterController.Move(Physics.gravity * Time.deltaTime);
        currentSpeed = Mathf.Lerp(currentSpeed, (transform.position - lastPosition).magnitude, 0.7f);
        lastPosition = transform.position;
        animator.SetFloat("Speed", currentSpeed);
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
