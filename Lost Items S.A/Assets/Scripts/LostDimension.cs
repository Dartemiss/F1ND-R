using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostDimension : MonoBehaviour
{
    public List<ConveyorBelt> conveyorBelts;
    public Stack<int> conveyorBeltRandomBag;

    FlamaTimer lostTimer;


    // Start is called before the first frame update
    void Start()
    {
        lostTimer = GetComponent<FlamaTimer>();
        conveyorBeltRandomBag = new Stack<int>();
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
        int conveyorBeltIndex = GetConveyorBeltIndex();
        int lostObjectTypeInt = Random.Range(0, 3);
        conveyorBelts[conveyorBeltIndex].SpawnLostObject((LostObject.LostObjectType)lostObjectTypeInt);
    }

    int GetConveyorBeltIndex()
    {
        if (conveyorBeltRandomBag.Count == 0)
        {
            FillRandomBag();
        }
        return conveyorBeltRandomBag.Pop();
    }

    void FillRandomBag()
    {
        List<int> conveyorBeltRandomArray = new List<int>();
        for (int i = 0; i < conveyorBelts.Count; ++i)
        {
            conveyorBeltRandomArray.Add(i);
        }

        for (int i = conveyorBeltRandomArray.Count - 1; i > 0; i--)
        {
            int swapIndex = Random.Range(0, i + 1);
            int tmp = conveyorBeltRandomArray[i];
            conveyorBeltRandomArray[i] = conveyorBeltRandomArray[swapIndex];
            conveyorBeltRandomArray[swapIndex] = tmp;
        }

        conveyorBeltRandomBag = new Stack<int>(conveyorBeltRandomArray);
    }
}
