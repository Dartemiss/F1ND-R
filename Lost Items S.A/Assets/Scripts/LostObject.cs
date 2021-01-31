using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostObject : MonoBehaviour
{

    Transform targetTransform;
    float currentTime = 0f;
    const float timeLerping = 0.235f;
    bool moving = false;
    Transform playerTransform;
    Vector3 startingPosition = Vector3.zero;

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

        if(moving)
        {

            currentTime += Time.deltaTime;
            float progress = currentTime / timeLerping;
            Vector3 pos = Vector3.Lerp(startingPosition, targetTransform.position, progress);

            transform.position = pos;

            if(progress >= 0.99f)
            {
                gameObject.transform.parent = playerTransform.transform;
                moving = false;
            }
        }

        if(transform.position.y < -20f)
        {
            Destroy(gameObject);
        }
    }

    public void AtractObject(Transform target, Transform player)
    {
        moving = true;
        startingPosition = transform.position;
        currentTime = 0f;
        targetTransform = target;
        playerTransform = player;
    }

    public void StopAtraction()
    {
        moving = false;
        gameObject.transform.parent = null;
    }
}
