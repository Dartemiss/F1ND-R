using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    LostObject currentLostObject = null;
    GameObject currentLostGameObject = null;

    public Transform objectSlot;
    public Transform colliderTransform;

    public LayerMask m_LayerMask;

    public GameObject currentTargetedObject = null;

    private bool carryingObject = false;

    private Animator animator;

    public ParticleSystem vfx;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        vfx.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        CheckObjectsInFront();
    }

    void CheckObjectsInFront()
    {

        Collider[] hitColliders = Physics.OverlapBox(
            colliderTransform.position,
            colliderTransform.localScale / 2,
            Quaternion.identity,
            m_LayerMask
        );
        
         int closerIndex = 0;
         float maxDistance = 10000f;
         bool found = false;
         for (int i = 0; i < hitColliders.Length; ++i)
         {
            bool isInteractableObject = false;
            isInteractableObject |= !carryingObject && hitColliders[i].gameObject.tag == "LostItem";
            isInteractableObject |= carryingObject && hitColliders[i].gameObject.tag == "CounterSlot";
            isInteractableObject |= hitColliders[i].gameObject.tag == "TableButton";

             if(isInteractableObject)
             {
                 
                 float distance = Vector3.Distance(hitColliders[i].gameObject.transform.position, transform.position);
                 if(distance < maxDistance)
                 {
                    maxDistance = distance;
                    closerIndex = i;
                    found = true;
                 }
             }
         }

        Outline outlineScript;
        if (hitColliders.Length == 0 || !found)
        {
            if (currentTargetedObject != null)
            {
                if(currentTargetedObject.tag == "CounterSlot")
                {
                    outlineScript = currentTargetedObject.transform.GetChild(0).gameObject.GetComponent<Outline>();
                }
                else
                {
                    outlineScript = currentTargetedObject.GetComponent<Outline>();
                }
                if (outlineScript != null)
                {
                    outlineScript.OutlineWidth = 0f;
                }
            }
            currentTargetedObject = null;
        }
        else
        {
            if (currentTargetedObject != null)
            {
                if(currentTargetedObject.tag == "CounterSlot")
                {
                    outlineScript = currentTargetedObject.transform.GetChild(0).gameObject.GetComponent<Outline>();
    
                }
                else
                {
                    outlineScript = currentTargetedObject.GetComponent<Outline>();
                }

                if (outlineScript != null)
                {
                    outlineScript.OutlineWidth = 0f;
                }
            }

            currentTargetedObject = hitColliders[closerIndex].gameObject;

            if(currentTargetedObject.tag == "CounterSlot")
            {
                outlineScript = currentTargetedObject.transform.GetChild(0).gameObject.GetComponent<Outline>();

            }
            else
            {
                outlineScript = currentTargetedObject.GetComponent<Outline>();
            }
            if (outlineScript != null)
            {
                outlineScript.OutlineWidth = 2f;
            }
        }
    }

    void PickUpObject()
    {
        if (currentTargetedObject == null)
        {
            return;
        }

        if (currentTargetedObject.tag == "LostItem")
        {
            LevelSoundManager.instance.PlayPickupSound();

            //Check if object is in Counter
            DeliverableTableController.instance.PickObject(currentTargetedObject);

            //currentTargetedObject.transform.parent = gameObject.transform;
            //currentTargetedObject.transform.position = objectSlot.position;
            currentLostObject = currentTargetedObject.GetComponent<LostObject>();
            currentLostObject.AtractObject(objectSlot, transform);
            currentTargetedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentLostGameObject = currentTargetedObject;
            carryingObject = true;
            animator.SetTrigger("TakingObject");
            vfx.Play();
        }
    }

    void PlaceObject()
    {
        if(currentLostGameObject != null)
        {
            animator.SetTrigger("GivingObject");
            LevelSoundManager.instance.PlayDropSound();

            //Place it on DeliverableTable
            if (currentTargetedObject != null && currentTargetedObject.tag == "CounterSlot")
            {
                if(!DeliverableTableController.instance.PutObject(currentLostGameObject, currentTargetedObject, currentLostObject))
                {
                    Debug.Log("Cannot place object here.");
                }
            }
            //Place it on the ground
            else
            {
                currentLostGameObject.transform.parent = null;
                currentLostGameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }


            currentLostObject = null;
            currentLostGameObject = null;
            carryingObject = false;
        }
    }

    public void InteractWithObject()
    {

        if(currentTargetedObject != null && currentTargetedObject.tag == "TableButton")
        {
            DeliverableTableController.instance.DeliverCommand();
            return;
        }


        if(!carryingObject)
        {
            PickUpObject();
        }
        else
        {
            PlaceObject();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode

        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(colliderTransform.position, colliderTransform.localScale/ 2);
    }

 
}
