using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{

    FlamaTimer commandTimer;
    List<LostObject.LostObjectType> commandItems = new List<LostObject.LostObjectType>();
    public int commandScore = 0;
    
    public void StartCommand(float time, List<LostObject.LostObjectType> items)
    {
        commandTimer = gameObject.GetComponent<FlamaTimer>();
        
        commandTimer.StartTimer(time);
        items = commandItems;
        items.Sort();

        commandScore = 300 + items.Count * 100;
    }

    public bool UpdateCommand()
    {
        if(commandTimer.HasTimedOut())
        {

            return false;
            
        }
        
        return true;
    }

    public bool CheckDeliver(List<LostObject.LostObjectType> deliveredItems)
    {
        if(deliveredItems.Count != commandItems.Count)
        {
            return false;
        }

        deliveredItems.Sort();

        for(int i = 0; i < deliveredItems.Count; ++i)
        {
            if(deliveredItems[i] != commandItems[i])
            {
                return false;
            }
        }

        return true;
    }
}


