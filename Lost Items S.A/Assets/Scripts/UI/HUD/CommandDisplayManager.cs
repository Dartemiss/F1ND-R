using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandDisplayManager : MonoBehaviour
{
    public bool available;
    public CommandController commandController;

    public List<Image> setup1Images;
    public List<Image> setup2Images;
    public List<Image> setup3Images;
    public Image bocadilloImage;

    int numSprites = 0;
    
    // Start is called before the first frame update
    void Awake()
    {
        Hide();
    }

    public void Show(List<Sprite> lostObjectsSprites, CommandController command)
    {
        numSprites = lostObjectsSprites.Count;

        for (int i = 0; i < lostObjectsSprites.Count; ++i)
        {
            Sprite sprite = lostObjectsSprites[i];
            if (numSprites == 1)
            {
                setup1Images[i].enabled = true;
                setup1Images[i].sprite = sprite;
            }
            else if (numSprites == 2)
            {
                setup2Images[i].enabled = true;
                setup2Images[i].sprite = sprite;
            }
            else if (numSprites == 3)
            {
                setup3Images[i].enabled = true;
                setup3Images[i].sprite = sprite;
            }
        }
        
        available = false;
        commandController = command;
        bocadilloImage.enabled = true;
    }

    public void Hide()
    {

        for (int i = 0; i < 3; ++i)
        {
            if (i < 1)
            {
                setup1Images[i].enabled = false;
            }
            if (i < 2)
            {
                setup2Images[i].enabled = false;
            }
            if (i < 3)
            {
                setup3Images[i].enabled = false;
            }
        }
        bocadilloImage.enabled = false;
        available = true;
        commandController = null;
    }

    public bool IsAvailable()
    {
        return available;
    }
}
