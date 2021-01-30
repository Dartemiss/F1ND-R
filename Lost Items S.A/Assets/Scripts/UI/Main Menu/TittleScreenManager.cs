using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TittleScreenManager : MonoBehaviour
{
    public static TittleScreenManager instance = null;

    //Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        transform.gameObject.SetActive(false);
    }

    public void Open()
    {
        transform.gameObject.SetActive(true);
    }
}
