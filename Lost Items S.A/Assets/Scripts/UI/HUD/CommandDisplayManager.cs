using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandDisplayManager : MonoBehaviour
{
    Image commandImage;

    bool available = true;

    // Start is called before the first frame update
    void Start()
    {
        commandImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(Sprite lostObjectSprite)
    {
        commandImage.sprite = lostObjectSprite;
        available = false;
    }

    public void Hide()
    {
        available = true;
    }

    public bool IsAvailable()
    {
        return available;
    }
}
