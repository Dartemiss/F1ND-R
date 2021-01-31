using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostObject : MonoBehaviour
{
    public enum LostObjectType
    {
        SOCK_YELLOW,
        SOCK_BLUE,
        SOCK_GREEN,
        DNI,
        GLASSES,
        KEYS,
        CONTROLLER_1,
        CONTROLLER_2,
        LIGHTER_1,
        LIGHTER_2,
        SMARTPHONE_1,
        PENDRIVE,
        NONE
    }

    public LostObjectType lostObjectType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }
}
