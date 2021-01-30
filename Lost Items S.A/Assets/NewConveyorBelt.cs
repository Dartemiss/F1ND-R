using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewConveyorBelt : MonoBehaviour
{
    public Rigidbody forceField;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        forceField.position -= transform.forward * speed * Time.deltaTime;
        forceField.MovePosition(forceField.position + transform.forward * speed * Time.deltaTime);
    }
}
