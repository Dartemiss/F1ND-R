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
        GLASSES_BLUE,
        GLASSES_RED,
        KEYS,
        CONTROLLER_WHITE,
        CONTROLLER_BLACK,
        LIGHTER_YELLOW,
        LIGHTER_RED,
        SMARTPHONE,
        PENDRIVE_YELLOW,
        PENDRIVE_PINK,
        PENDRIVE_GREEN,
        CREDIT_CARD,
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
