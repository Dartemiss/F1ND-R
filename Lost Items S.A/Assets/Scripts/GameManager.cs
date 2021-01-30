using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isRunning; 

    private FlamaTimer levelTimer;
    public int currentScore = 0;
    float totalLevelTime = 300f;

    List<float> goalStarScores = new List<float>(){2500, 4500, 10000};
    int numberOfStars = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            isRunning = false;
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
        if(!isRunning)
        {
            return;
        }

        HUDManager.instance.UpdateTime(levelTimer.currentTimeRemaining);
        HUDManager.instance.UpdateScore(currentScore);

        if(levelTimer.HasTimedOut())
        {
            //Compute stars
            for(int i = goalStarScores.Count - 1; i >= 0; --i)
            {
                if(currentScore >= goalStarScores[i])
                {
                    numberOfStars = i + 1;
                    break;
                }
            }


            //End game
            isRunning = false;
            SceneManager.LoadScene(0);
        }
    }

    public void AddScore(int scoreBonus)
    {
        currentScore += scoreBonus;
    }

    public void SubstractScore(int scorePenalty)
    {
        currentScore -= scorePenalty;
    }

    public void LoadMainLevel()
    {
        isRunning = true;
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
