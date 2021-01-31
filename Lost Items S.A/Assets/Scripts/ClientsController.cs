using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ClientsController : MonoBehaviour
{
    const uint maxTypeOfObjects = 16;
    List<CommandController> commands = new List<CommandController>();

    public GameObject commandPrefab;
    public DeliverableTableController counterController;
    public CommandsDisplayManager displayerManager;

    uint minNumberOfObjectsPerCommand = 1;
    uint maxNumberOfObjectsPerCommand = 1;

    uint maxCommands = 3;
    public uint numSuccesCommands = 0;
    FlamaTimer nextCommandTimer;

    float timeForNextTask = 20f;

    uint numOfCommandsPerCreation = 1;

    public LostDimension lostDimension;

    int consecutiveSucceses = 0;

    // Start is called before the first frame update
    void Awake()
    {
       nextCommandTimer = transform.gameObject.AddComponent<FlamaTimer>();
       nextCommandTimer.StartTimer(timeForNextTask); 
    }

    void Start()
    {
       for(int i = 0; i < 3; i++)
       {
           CreateCommand();
       }
    }

    // Update is called once per frame
    void Update()
    {
        if(commands.Count <= 0)
        {
            for(int i = 0; i < numOfCommandsPerCreation; ++i)
            {
                CreateCommand();
            }
            nextCommandTimer.Reset();
        }


        if(nextCommandTimer.HasTimedOut())
        {
            for(int i = 0; i < numOfCommandsPerCreation; ++i)
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
                LevelSoundManager.instance.PlayWrongSound();

                consecutiveSucceses = 0;
                break;
            }
        }
    }

    public void DeliverCommand()
    {
        bool success = false;
        int commandScore = 0;

        for(int i = 0; i < commands.Count; ++i)
        {
            if(commands[i].CheckDeliver(counterController.objectsTypes))
            {
                //Success
                success = true;
                commandScore = commands[i].commandScore;

                displayerManager.EraseCommand(commands[i]);
                commands[i].DestroyCommand();
                commands.RemoveAt(i);
                break;
            }
        }

        if(success)
        {
            //Earn points
            numSuccesCommands++;
            consecutiveSucceses++;
            commandScore += consecutiveSucceses * 15;
            GameManager.instance.AddScore(commandScore);
            LevelSoundManager.instance.PlayCorrectSound();
            CheckDifficulty();
        }
        else
        {
            //Lose points
            consecutiveSucceses = 0;
            GameManager.instance.SubstractScore(50);
            LevelSoundManager.instance.PlayWrongSound();
        }

        counterController.ClearTable();
    }

    void CreateCommand()
    {
        if(commands.Count >= maxCommands)
        {
            return;
        }
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
        if(numSuccesCommands == 12)
        {
            maxNumberOfObjectsPerCommand = 3;
            minNumberOfObjectsPerCommand = 3;
            timeForNextTask -= 2f;
            lostDimension.minTimePerSpawn = 0.25f;
            lostDimension.maxTimePerSpawn = 0.75f;
        }
        else if(numSuccesCommands == 6)
        {
            maxNumberOfObjectsPerCommand = 3;
            minNumberOfObjectsPerCommand = 2;
            timeForNextTask -= 1f;
            lostDimension.minTimePerSpawn = 0.5f;
        }
        else if(numSuccesCommands == 3)
        {
            maxNumberOfObjectsPerCommand = 2;
            timeForNextTask -= 1f;
            lostDimension.maxTimePerSpawn = 1f;
        }

        nextCommandTimer.SetTotalTime(timeForNextTask);
    }
           
}
