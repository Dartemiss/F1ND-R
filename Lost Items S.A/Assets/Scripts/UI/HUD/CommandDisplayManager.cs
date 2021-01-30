using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandDisplayManager : MonoBehaviour
{
    Image commandImage;

    bool available = true;

    public CommandController commandController;

    // Start is called before the first frame update
    void Start()
    {
        commandImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Sprite lostObjectSprite, CommandController command)
    {
        commandImage.sprite = lostObjectSprite;
        available = false;
        commandController = command;
    }

    public void Hide()
    {
        commandImage.sprite = null;
        available = true;
        commandController = null;
    }

    public bool IsAvailable()
    {
        return available;
    }
}
