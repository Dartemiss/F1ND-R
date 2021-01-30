using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{

    private bool isStarted = false;
    // Start is called before the first frame update
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int maxPlayers = 3;
    private int currentPlayers = 0;

    public static PlayerConfigurationManager Instance { get; private set; }

    private PlayerInputManager playerInputManager;

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

    public void SetPlayerMaterial(int index, Material material)
    {
        playerConfigs[index].PlayerMaterial = material;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count == maxPlayers && playerConfigs.All(p => p.IsReady == true))
        {
            GameManager.instance.LoadMainLevel();
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
         if (currentPlayers > maxPlayers || !isStarted) { return; }
       
        Debug.Log("Player Joined " + pi.playerIndex);
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
            ++currentPlayers;
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigurations()
    {
        return playerConfigs;
    }

    public void StartPlayerConfiguration()
    {
        isStarted = true;
        playerInputManager.EnableJoining();
    }
}



public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;

    }

    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }

    public bool IsReady { get; set; }

    public Material PlayerMaterial { get; set; }
}