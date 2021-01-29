using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClientsController : MonoBehaviour
{
    const uint maxTypeOfObjects = 3;
    List<CommandController> commands = new List<CommandController>();
    List<LostObject.LostObjectType> safataBro = new List<LostObject.LostObjectType>();

    public GameManager gameManager;

    public GameObject commandPrefab;

    uint minNumberOfObjectsPerCommand = 1;
    uint maxNumberOfObjectsPerCommand = 1;

    // Start is called before the first frame update
    void Start()
    {
       CreateCommand();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < commands.Count; ++i)
        {
            bool timeOut = commands[i].UpdateCommand();

            if(timeOut)
            {
                //Lose points
                gameManager.SubstractScore(50);

                commands.RemoveAt(i);
                break;
            }
        }
    }

    void DeliverCommand()
    {
        bool succes = false;
        int commandScore = 0;

        for(int i = 0; i < commands.Count; ++i)
        {
            if(commands[i].CheckDeliver(safataBro))
            {
                //Succes
                succes = true;
                commandScore = commands[i].commandScore;

                commands.RemoveAt(i);
                break;
            }
        }

        if(succes)
        {
            //Earn points
            gameManager.AddScore(commandScore);
        }
        else
        {
            //Lose points
            gameManager.SubstractScore(50);
        }
    }

    void CreateCommand()
    {
        uint numOfObjects = (uint)Random.Range(minNumberOfObjectsPerCommand, maxNumberOfObjectsPerCommand);
        List<LostObject.LostObjectType> commandItems = new List<LostObject.LostObjectType>();
    
        for(int i = 0; i < numOfObjects; ++i)
        {
            CreateObject(ref commandItems);
        }

        float time = 15f * numOfObjects + Random.Range(0, 30f);

        GameObject commandObject = Instantiate(commandPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        CommandController command = commandObject.GetComponent<CommandController>();
        command.StartCommand(30f, commandItems);

        commands.Add(command);

    }

    void CreateObject(ref List<LostObject.LostObjectType> commandItems)
    {
        LostObject.LostObjectType randomID = (LostObject.LostObjectType)Random.Range(0, maxTypeOfObjects);
        commandItems.Add(randomID);
    }
}
