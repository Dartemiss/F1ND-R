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
            Debug.Log("You gained " + numberOfStars +  "stars, congratulations!");
            SceneManager.LoadScene(0);
            PlayerConfigurationManager.Instance.RestartDevices();
        }
        else if(levelTimer.GetTimeRemaining() <= 30f)
        {
            LevelSoundManager.instance.PlayAlarmSouns();
        }
    }

    public void AddScore(int scoreBonus)
    {
        currentScore += scoreBonus;
    }

    public void SubstractScore(int scorePenalty)
    {
        currentScore -= scorePenalty;
        if(currentScore < 0)
        {
            currentScore = 0;
        }
    }

    public void LoadMainLevel()
    {
        PlayerConfigurationManager.Instance.playerInputManager.DisableJoining();
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        levelTimer.StartTimer(totalLevelTime);
        isRunning = true;
        
        GameObject.Find("Clients").GetComponent<ClientsController>().CreateFirstCommands();
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
