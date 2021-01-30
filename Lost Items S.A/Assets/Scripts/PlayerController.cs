using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    LostObject currentLostObject = null;
    GameObject currentLostGameObject = null;

    Transform objectSlot;
    Transform colliderTransform;

    public LayerMask m_LayerMask;

    public GameObject currentTargetedObject = null;

    public bool triggerAction = false;
    private bool carryingObject = false;

    // Start is called before the first frame update
    void Start()
    {
        objectSlot = gameObject.transform.GetChild(0).transform;
        colliderTransform = gameObject.transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObjectsInFront();
        if(triggerAction)
        {
            Debug.Log("Picking up object.");
            PickUpObject();
            PlaceObject();
        }
    }

    void CheckObjectsInFront()
    {

         Collider[] hitColliders = Physics.OverlapBox(
            colliderTransform.position,
              colliderTransform.localScale / 2,
               Quaternion.identity,
                m_LayerMask);

         int i = 0;
         int closerIndex = 0;
         float maxDistance = 10000f;
         bool found = false;
         while(i < hitColliders.Length)
         {
             bool condition = (!carryingObject) ? hitColliders[i].gameObject.tag == "LostItem" : hitColliders[i].gameObject.tag == "CounterSlot";

             if(condition)
             {
                 float distance = Vector3.Distance(hitColliders[i].gameObject.transform.position, transform.position);
                 if(distance < maxDistance)
                 {
                    maxDistance = distance;
                    closerIndex = i;
                    found = true;
                 }
             }

             ++i;
         }
        
        if(hitColliders.Length == 0 || !found)
        {
            currentTargetedObject = null;
            return;
        }

         currentTargetedObject = hitColliders[closerIndex].gameObject; 
    }

    void PickUpObject()
    {
        if(currentTargetedObject != null && !carryingObject && currentTargetedObject.tag == "LostItem")
        {
            currentTargetedObject.transform.parent = gameObject.transform;
            currentTargetedObject.transform.position = objectSlot.position;
            currentLostObject = currentTargetedObject.GetComponent<LostObject>();
            currentLostGameObject = currentTargetedObject;
            carryingObject = true;
        }
    }

    void PlaceObject()
    {
        if(currentTargetedObject != null && currentLostGameObject != null && carryingObject && currentTargetedObject.tag == "CounterSlot")
        {
            currentLostGameObject.transform.parent = currentTargetedObject.transform;
            currentLostGameObject.transform.position = currentTargetedObject.transform.position;
            currentLostObject = null;
            currentLostGameObject = null;
            carryingObject = false;
        }
    }
 
}
