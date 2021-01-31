using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("SINGLETON PCM - Trying to create another one");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
            playerInputManager = GetComponent<PlayerInputManager>();
        }
    }

    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int maxPlayers = 3;
    private int currentPlayers = 0;

    public PlayerInputManager playerInputManager;
    bool started = false;

    public List<Material> playerMaterials;

    public void HandlePlayerJoin(PlayerInput playerInput)
    {
        if (!started)
        {
            started = true;
            MainMenuManager.instance.OpenPlayerSelectionScreen();
        }

        Debug.Log("Trying to joing player");

        if (currentPlayers > maxPlayers)
        {
            return;
        }
       
        if (!playerConfigs.Any(playerConfig => playerConfig.PlayerIndex == playerInput.playerIndex))
        {
            Debug.Log("Player Joined " + playerInput.playerIndex);
            playerInput.transform.SetParent(transform);
            PlayerConfiguration createdPlayerConfig = new PlayerConfiguration(playerInput);
            PlayerCardManager cardManager = playerInput.GetComponent<SpawnPlayerCardManager>().AssignPlayerCard();
            createdPlayerConfig.PlayerMaterial = playerMaterials[(int)cardManager.assignedColor];
            playerConfigs.Add(createdPlayerConfig);
            if (playerInput.playerIndex == 0)
            {
                playerInput.uiInputModule = MainMenuManager.instance.GetStartGameUIInput();

                PlayerSelectionManager.InputType inputType;
                if (playerInput.currentControlScheme == "Keyboard")
                {
                    inputType = PlayerSelectionManager.InputType.KEYBOARD;
                }
                else if (playerInput.currentControlScheme == "Gamepad")
                {
                    inputType = PlayerSelectionManager.InputType.CONTROLLER;
                }
                else
                {
                    inputType = PlayerSelectionManager.InputType.NONE;
                }
                MainMenuManager.instance.SetPlayerOneInputType(inputType);
            }

            ++currentPlayers;
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigurations()
    {
        return playerConfigs;
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput playerInput)
    {
        PlayerIndex = playerInput.playerIndex;
        Input = playerInput;

    }

    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }

    public bool IsReady { get; set; }

    public Material PlayerMaterial { get; set; }
}