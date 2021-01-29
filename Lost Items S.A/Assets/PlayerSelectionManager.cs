using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionManager : MonoBehaviour
{
    public enum InputType
    {
        KEYBOARD,
        CONTROLLER
    }

    public List<PlayerCardManager> playerCardManagers;
    int lastActivatedCardManager = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewController()
    {
        ++lastActivatedCardManager;
        playerCardManagers[lastActivatedCardManager].Activate(InputType.CONTROLLER);
    }

    public void AddNewKeyboard()
    {
        ++lastActivatedCardManager;
        playerCardManagers[lastActivatedCardManager].Activate(InputType.KEYBOARD);
    }
}
