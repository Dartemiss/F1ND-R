using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{

    FlamaTimer commandTimer;
    
    public List<LostObject.LostObjectType> commandItems = new List<LostObject.LostObjectType>();
    public int commandScore = 0;
    
    public void StartCommand(float time, List<LostObject.LostObjectType> items)
    {
        commandTimer = gameObject.GetComponent<FlamaTimer>();
        
        commandTimer.StartTimer(time);
        commandItems = items;
        items.Sort();

        commandScore = 100 + items.Count * 50;
    }

    public bool UpdateCommand()
    {
        if(commandTimer.HasTimedOut())
        {

            return true;
            
        }
        
        return false;
    }

    public bool CheckDeliver(List<LostObject.LostObjectType> deliveredItems)
    {

        int numObjects = 0;
        foreach(LostObject.LostObjectType item in deliveredItems)
        {
            if(item != LostObject.LostObjectType.NONE)
            {
                ++numObjects;
            }
        }

        if(numObjects != commandItems.Count)
        {
            return false;
        }

        deliveredItems.Sort();

        for(int i = 0; i < numObjects; ++i)
        {
            if(deliveredItems[i] != commandItems[i])
            {
                return false;
            }
        }

        return true;
    }

    public void DestroyCommand()
    {
        Destroy(gameObject);
    }
}


