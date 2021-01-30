using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandDisplayManager : MonoBehaviour
{

    public List<Image> lostObjectsImages;
    public bool available;

    public CommandController commandController;

    // Start is called before the first frame update
    public void AwakeCommand()
    {
        for(int i = 0; i < 3; ++i)
        {
            lostObjectsImages.Add(transform.GetChild(i).gameObject.GetComponent<Image>());
        }

        Hide();
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
