using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverableTableController : MonoBehaviour
{
    public List<LostObject.LostObjectType> objectsTypes = new List<LostObject.LostObjectType>();

    List<Transform> counterSlots = new List<Transform>(); 
    List<bool> availableSlots = new List<bool>(){true, true, true};
    List<GameObject> counterGameObjects = new List<GameObject>() {null, null, null};


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < gameObject.transform.childCount - 1; ++i)
        {
            counterSlots.Add(gameObject.transform.GetChild(i).transform);
            counterGameObjects.Add(gameObject.transform.GetChild(i).transform.GetChild(0).gameObject);
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

    bool PutObject(int indexCounter, GameObject lostObject)
    {
        if(availableSlots[indexCounter] == false)
        {
            return false;
        }

        lostObject.transform.position = counterSlots[indexCounter].position;
        availableSlots[indexCounter] = false;
        counterGameObjects[indexCounter] = lostObject;

        return true;
    }

    bool PickObject(int indexCounter, ref GameObject lostObject)
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
