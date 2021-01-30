using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ClientsController : MonoBehaviour
{
    const uint maxTypeOfObjects = 3;
    List<CommandController> commands = new List<CommandController>();

    public GameObject commandPrefab;
    public DeliverableTableController counterController;
    public CommandsDisplayManager displayerManager;

    uint minNumberOfObjectsPerCommand = 1;
    uint maxNumberOfObjectsPerCommand = 1;

    uint maxCommands = 5;
    public uint numSuccesCommands = 0;
    FlamaTimer nextCommandTimer;

    // Start is called before the first frame update
    void Start()
    {
       CreateCommand();

       nextCommandTimer = transform.gameObject.AddComponent<FlamaTimer>();
       nextCommandTimer.StartTimer(10f); 
    }

    // Update is called once per frame
    void Update()
    {
        if(nextCommandTimer.HasTimedOut())
        {
            if(commands.Count < maxCommands)
            {
                CreateCommand();
            }
            nextCommandTimer.Reset();
        }

        for(int i = 0; i < commands.Count; ++i)
        {
            bool timeOut = commands[i].UpdateCommand();

            if(timeOut)
            {
                //Lose points
                GameManager.instance.SubstractScore(50);
                displayerManager.EraseCommand(commands[i]);
                commands[i].DestroyCommand();
                commands.RemoveAt(i);


                break;
            }
        }
    }

    public void DeliverCommand()
    {
        bool succes = false;
        int commandScore = 0;

        for(int i = 0; i < commands.Count; ++i)
        {
            if(commands[i].CheckDeliver(counterController.objectsTypes))
            {
                //Succes
                succes = true;
                commandScore = commands[i].commandScore;

                displayerManager.EraseCommand(commands[i]);
                commands[i].DestroyCommand();
                commands.RemoveAt(i);
                break;
            }
        }

        if(succes)
        {
            //Earn points
            GameManager.instance.AddScore(commandScore);
            numSuccesCommands++;
            Debug.Log("Earning points");
            CheckDifficulty();
        }
        else
        {
            //Lose points
            GameManager.instance.SubstractScore(50);
             Debug.Log("Losing points");
        }

        counterController.ClearTable();
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
        
        float diff = Random.Range(-10f, 10f);
        command.StartCommand(45f + diff, commandItems);

        commands.Add(command);
        displayerManager.ShowCommand(command);
    }

    void CreateObject(ref List<LostObject.LostObjectType> commandItems)
    {
        LostObject.LostObjectType randomID = (LostObject.LostObjectType)Random.Range(0, maxTypeOfObjects);
        commandItems.Add(randomID);
    }

    void CheckDifficulty()
    {
        if(numSuccesCommands > 12)
        {
            maxNumberOfObjectsPerCommand = 3;
            minNumberOfObjectsPerCommand = 2;
        }
        else if(numSuccesCommands > 8)
        {
            maxNumberOfObjectsPerCommand = 3;
        }
        else if(numSuccesCommands > 3)
        {
            maxNumberOfObjectsPerCommand = 2;
        }
    }
           
}
