using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverableTableController : MonoBehaviour
{
    public List<LostObject.LostObjectType> objectsTypes = new List<LostObject.LostObjectType>();

    List<Transform> counterSlots = new List<Transform>(); 
    List<bool> availableSlots = new List<bool>(){true, true, true};
    List<GameObject> counterGameObjects = new List<GameObject>() {null, null, null};
    public List<GameObject> slotsGameObjects = new List<GameObject>();

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

        Debug.Log("1");
        lostObject.transform.position = counterSlots[indexCounter].position;
        lostObject.transform.parent = transform;
        Debug.Log("2");
        objectsTypes[indexCounter] = currentLostObject.lostObjectType;
        Debug.Log("3");
        availableSlots[indexCounter] = false;
        Debug.Log("4");
        counterGameObjects[indexCounter] = lostObject;
        Debug.Log("5");

        return true;
    }

    public bool PickObject(int indexCounter, ref GameObject lostObject)
    {
        if(availableSlots[indexCounter] == true)
        {
            return false;
        }

        availableSlots[indexCounter] = true;
        lostObject = counterGameObjects[indexCounter];
        counterGameObjects[indexCounter] = null;

        return true;
    }
    
}
