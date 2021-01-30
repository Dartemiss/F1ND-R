using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{

    private int playerIndex;

    [SerializeField]
    private Text playerTitle;

    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private GameObject readyPanel;

    [SerializeField]
    private Button readyButton;

    private float ignoreInputTIme = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        playerTitle.text = "Player " + (pi + 1).ToString();
        ignoreInputTIme += Time.time;
    }

    private void Update()
    {
        if(Time.time > ignoreInputTIme)
        {
            inputEnabled = true;
        }
    }

    public void SetMaterial(Material material)
    {
        if (!inputEnabled) { return;}

        PlayerConfigurationManager.Instance.SetPlayerMaterial(playerIndex, material);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);

    }

    public void ReadyPlayer()
    {
        if(!inputEnabled) { return; }

        PlayerConfigurationManager.Instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
    }

}
