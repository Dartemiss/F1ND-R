using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorBelt : MonoBehaviour
{
    public float beltSpeed;
    public Rigidbody forceField;

    List<GameObject> conveyorBeltObjects;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        forceField.position -= transform.forward * beltSpeed * Time.deltaTime;
        forceField.MovePosition(forceField.position + transform.forward * beltSpeed * Time.deltaTime);
    }
}
