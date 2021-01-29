using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager _instance;

    private FlamaTimer levelTimer;
    float currentScore = 0f;
    float totalLevelTime = 300f;

    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
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
}
