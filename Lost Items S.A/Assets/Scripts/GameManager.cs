using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private FlamaTimer levelTimer;
    float currentScore = 0f;
    float totalLevelTime = 300f;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

        levelTimer = transform.gameObject.AddComponent<FlamaTimer>();
        levelTimer.StartTimer(totalLevelTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(levelTimer.HasTimedOut())
        {
            //Lose
            Debug.Log("You lost little useless noob.");
        }
    }

    public void AddScore(float scoreBonus)
    {
        currentScore += scoreBonus;
    }

    public void SubstractScore(float scorePenalty)
    {
        currentScore -= scorePenalty;
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
