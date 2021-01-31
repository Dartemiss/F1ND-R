using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsDisplayManager : MonoBehaviour
{
    public List<Sprite> lostObjectSprites;
    public List<CommandDisplayManager> commandDisplayManagers;

    void Awake()
    {
    }

    public void ShowCommand(CommandController command)
    {
        int i = GetAvailableCommandDisplay();
        if (i == -1)
        {
            Debug.Log("No command display available");
            return;
        }

        List<Sprite> sprites = new List<Sprite>();
        for(int j = 0; j < command.commandItems.Count; ++j)
        {
            sprites.Add(lostObjectSprites[(int)command.commandItems[j]]);
        }
        commandDisplayManagers[i].Show(sprites, command);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float commandDisplayManagerXPos = rectTransform.rect.width / 2f -  Random.Range(50, rectTransform.rect.width - 50);
        float commandDisplayManagerYPos = rectTransform.rect.height / 2f - Random.Range(50, rectTransform.rect.height - 50);
        commandDisplayManagers[i].transform.localPosition = new Vector3(commandDisplayManagerXPos, commandDisplayManagerYPos, 0);
    }

    public void EraseCommand(CommandController command)
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
