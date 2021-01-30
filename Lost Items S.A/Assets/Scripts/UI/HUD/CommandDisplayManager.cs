using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandDisplayManager : MonoBehaviour
{

    public List<Image> lostObjectsImages;
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

    public void Show(List<Sprite> lostObjectsSprites, CommandController command)
    {
        int i = 0;
        foreach(Sprite sprite in lostObjectsSprites)
        {
            lostObjectsImages[i].enabled = true;
            lostObjectsImages[i].sprite = sprite;
            ++i;
        }

        available = false;
        commandController = command;
    }

    public void Hide()
    {
        foreach(Image image in lostObjectsImages)
        {
            image.enabled = false;
        }

        available = true;
        commandController = null;
    }

    public bool IsAvailable()
    {
        return available;
    }
}
