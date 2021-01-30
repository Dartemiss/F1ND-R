using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverableTableController : MonoBehaviour
{
    public List<LostObject.LostObjectType> objectsTypes = new List<LostObject.LostObjectType>();

    List<Transform> counterSlots = new List<Transform>(); 
    public List<bool> availableSlots = new List<bool>(){true, true, true};
    public List<GameObject> counterGameObjects = new List<GameObject>() {null, null, null};
    public List<GameObject> slotsGameObjects = new List<GameObject>();

    public ClientsController clientController;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < gameObject.transform.childCount - 1; ++i)
        {
            counterSlots.Add(gameObject.transform.GetChild(i).transform);
            slotsGameObjects.Add(gameObject.transform.GetChild(i).transform.GetChild(0).gameObject);
            objectsTypes.Add(LostObject.LostObjectType.NONE);
        }
    }

    public void ClearTable()
    {
        for(int i = 0; i < counterGameObjects.Count; ++i)
        {   
            GameObject auxiliarObject = counterGameObjects[i];
            counterGameObjects[i] = null;
            objectsTypes[i] = LostObject.LostObjectType.NONE;
            availableSlots[i] = true;

            Destroy(auxiliarObject);
        }
    }

    public bool PutObject(GameObject lostObject, GameObject slotObject,  LostObject currentLostObject)
    {
        bool existsSlot = false;

        int indexCounter = 0;
        foreach(GameObject slot in slotsGameObjects)
        {
            if(slot == slotObject)
            {
                existsSlot = true;
                break;
            }
            ++indexCounter;
        }

        if(!existsSlot ||  !availableSlots[indexCounter])
        {
            return false;
        }

        lostObject.transform.position = counterSlots[indexCounter].position;
        lostObject.transform.parent = transform;
        objectsTypes[indexCounter] = currentLostObject.lostObjectType;
        availableSlots[indexCounter] = false;
        counterGameObjects[indexCounter] = lostObject;

        return true;
    }

    public void PickObject(GameObject lostObject)
    {
        int indexCounter = 0;
        foreach(GameObject counterGameObject in counterGameObjects)
        {
            if(counterGameObject == lostObject)
            {
                availableSlots[indexCounter] = true;
                counterGameObjects[indexCounter] = null;
                objectsTypes[indexCounter] = LostObject.LostObjectType.NONE;
                return;
            }
            ++indexCounter;
        }
    }

    public void DeliverCommand()
    {
        clientController.DeliverCommand();
    }
    
}
