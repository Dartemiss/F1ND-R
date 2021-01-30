using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandDisplayManager : MonoBehaviour
{
    public Image lostObject1Image;
    public Image lostObject2Image;
    public Image lostObject3Image;

    bool available = true;

    public CommandController commandController;

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Sprite lostObject1Sprite, Sprite lostObject2Sprite, Sprite lostObject3Sprite, CommandController command)
    {

        lostObject1Image.enabled = true;
        lostObject2Image.enabled = true;
        lostObject3Image.enabled = true;

        lostObject1Image.sprite = lostObject1Sprite;
        lostObject2Image.sprite = lostObject2Sprite;
        lostObject3Image.sprite = lostObject3Sprite;

        available = false;
        commandController = command;
    }

    public void Hide()
    {
        lostObject1Image.enabled = false;
        lostObject2Image.enabled = false;
        lostObject3Image.enabled = false;

        available = true;
        commandController = null;
    }

    public bool IsAvailable()
    {
        return available;
    }
}
