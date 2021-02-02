using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FinalScore : MonoBehaviour
{

    //Assets
    int Nstars;
    //Star images
    public Image Star1;
    public Image Star2;
    public Image Star3;

    //Colors for stars
    public Color White;
    public Color Gray;

    // Start is called before the first frame update
    void Start()
    {
        Nstars = GameManager.instance.GetNumberStars();
        Star1 = GameObject.Find("Star1").GetComponent<Image>();
        Star2 = GameObject.Find("Star2").GetComponent<Image>();
        Star3 = GameObject.Find("Star3").GetComponent<Image>();
       switch (Nstars)
        {
            case 0:
                Star1.color = Gray;
                Star2.color = Gray;
                Star3.color = Gray;
                break;

            case 1:
                Star1.color = White;
                Star2.color = Gray;
                Star3.color = Gray;
                break;
            case 2:
                Star1.color = White;
                Star2.color = White;
                Star3.color = Gray;

                break;
            case 3:
                Star1.color = White;
                Star2.color = White;
                Star3.color = White;
                break;

            default:
                break;
        }
        PlayerConfigurationManager.Instance.RestartDevices();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToMainMenu()
    {
        Debug.Log("Loading MainMenu");
        SceneManager.LoadScene(0);
        //        PlayerConfigurationManager.Instance.RestartDevices(); if i leave that here it will render the bttn useless dunno why :(
    }
    public void FinishGame()
    {
        Debug.Log("quit app");
        Application.Quit();
    }
}
