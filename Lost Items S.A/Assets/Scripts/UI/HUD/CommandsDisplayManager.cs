using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsDisplayManager : MonoBehaviour
{
    public List<Sprite> lostObjectSprites;
    public List<CommandDisplayManager> commandDisplayManagers;

    // Start is called before the first frame update
    void Start()
    {
        //Mario loves u
    }

    // Update is called once per frame
    void Update()
    {
        //Mario hates u
    }

    public void ShowCommand(CommandController command)
    {
        Sprite lostObjectSprite = lostObjectSprites[(int)command.commandItems[0]];
        int i = GetAvailableCommandDisplay();
        if (i == -1)
        {
            Debug.Log("No command display available");
            return;
        }
        commandDisplayManagers[i].Show(lostObjectSprite, command);

    }

    public void  EraseCommand(CommandController command)
    {
        for(int i = 0; i < commandDisplayManagers.Count; ++i)
        {
            if(commandDisplayManagers[i].commandController == command)
            {
                commandDisplayManagers[i].Hide();
                break;
            }
        }
    }

    int GetAvailableCommandDisplay()
    {
        int i = 0;
        foreach (CommandDisplayManager commandDisplayManager in commandDisplayManagers)
        {
            if (commandDisplayManager.IsAvailable())
            {
                return i;
            }
            ++i;
        }

        return -1;
    }
}
