using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostDimension : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public Stack<int> spawnPointsRandomBag;

    FlamaTimer lostTimer;


    // Start is called before the first frame update
    void Start()
    {
        lostTimer = GetComponent<FlamaTimer>();
        spawnPointsRandomBag = new Stack<int>();
        SetNextObjectTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (lostTimer.HasTimedOut())
        {
            SpawnLostObject();
            SetNextObjectTimer();
        }
    }

    void SetNextObjectTimer()
    {
        lostTimer.StartTimer(Random.Range(2, 3));
    }

    void SpawnLostObject()
    {
        int conveyorBeltIndex = GetSpawnPointIndex();
        int lostObjectTypeInt = Random.Range(0, 3);
        SpawnLostObject((LostObject.LostObjectType)lostObjectTypeInt, spawnPoints[conveyorBeltIndex]);
    }

    int GetSpawnPointIndex()
    {
        if (spawnPointsRandomBag.Count == 0)
        {
            FillRandomBag();
        }
        return spawnPointsRandomBag.Pop();
    }

    void FillRandomBag()
    {
        List<int> spawnPointRandomArray = new List<int>();
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            spawnPointRandomArray.Add(i);
        }

        for (int i = spawnPointRandomArray.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            int tmp = spawnPointRandomArray[i];
            spawnPointRandomArray[i] = spawnPointRandomArray[swapIndex];
            spawnPointRandomArray[swapIndex] = tmp;
        }

        spawnPointsRandomBag = new Stack<int>(spawnPointRandomArray);
    }

    void SpawnLostObject(LostObject.LostObjectType lostObjectType, Transform spawnPosition)
    {
        GameObject spawnedGameObject = LostObjectFactory.instance.CreateLostObject(lostObjectType);
        spawnedGameObject.transform.position = spawnPosition.position;
    }

}
