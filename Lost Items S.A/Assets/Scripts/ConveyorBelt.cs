using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorBelt : MonoBehaviour
{
    public Transform lostObjectSpawnPoint;
    public List<Transform> beltPath;
    public float beltSpeed;

    public struct ConveyorBeltObject
    {
        public GameObject lostObject;
        public int currentTimeStampIndex;

        public float elapsedTime;

        public ConveyorBeltObject(GameObject lostObject, int currentTimeStampIndex)
        {
            this.lostObject = lostObject;
            this.currentTimeStampIndex = currentTimeStampIndex;

            elapsedTime = 0f;
        }
    }

    public List<float> beltTimeStamps;
    List<ConveyorBeltObject> conveyorBeltObjects;
    

    // Start is called before the first frame update
    void Start()
    {
        conveyorBeltObjects = new List<ConveyorBeltObject>();
        beltTimeStamps = new List<float>();

        for (int i = 0; i < beltPath.Count; ++i)
        {
            if (i == 0)
            {
                beltTimeStamps.Add(0);
                
            }
            else
            {
                Vector3 lastPointPosition = beltPath[i - 1].position;
                Vector3 currentPointPosition = beltPath[i].position;
                float distanceBetweenPoints = Vector3.Distance(lastPointPosition, currentPointPosition);
                float timeBetweenPoints = distanceBetweenPoints / beltSpeed;
                beltTimeStamps.Add(beltTimeStamps[i - 1] + timeBetweenPoints);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            SpawnLostObject(LostObject.LostObjectType.FOO_1);
        }

        List<ConveyorBeltObject> objectsToRemove = new List<ConveyorBeltObject>();
        for (int i = 0; i < conveyorBeltObjects.Count; ++i)
        {
            ConveyorBeltObject spawnedLostObject = conveyorBeltObjects[i];

            spawnedLostObject.elapsedTime = spawnedLostObject.elapsedTime + Time.deltaTime;
            while (
                spawnedLostObject.currentTimeStampIndex < beltTimeStamps.Count - 1
                && beltTimeStamps[spawnedLostObject.currentTimeStampIndex + 1]  < spawnedLostObject.elapsedTime 
            )
            {
                ++spawnedLostObject.currentTimeStampIndex;
            }


            if (spawnedLostObject.currentTimeStampIndex == beltTimeStamps.Count - 1)
            {
                objectsToRemove.Add(conveyorBeltObjects[i]);

            }
            else
            {
                float previousTimeStamp = beltTimeStamps[spawnedLostObject.currentTimeStampIndex];
                float nextTimeStamp = beltTimeStamps[spawnedLostObject.currentTimeStampIndex + 1];
                float progress = (spawnedLostObject.elapsedTime - previousTimeStamp) / (nextTimeStamp - previousTimeStamp);

                Vector3 previousBeltPosition = beltPath[spawnedLostObject.currentTimeStampIndex].position;
                Vector3 nextBeltPosition = beltPath[spawnedLostObject.currentTimeStampIndex + 1].position;
                spawnedLostObject.lostObject.transform.position = Vector3.Lerp(previousBeltPosition, nextBeltPosition, progress);

                conveyorBeltObjects[i] = spawnedLostObject;
            }
        }

        conveyorBeltObjects.RemoveAll(objectsToRemove.Contains);
        foreach (ConveyorBeltObject objectToRemove in objectsToRemove)
        {
            Destroy(objectToRemove.lostObject);
        }
    }

    public void SpawnLostObject(LostObject.LostObjectType lostObjectType)
    {
        GameObject spawnedGameObject = LostObjectFactory.instance.CreateLostObject(lostObjectType);
        spawnedGameObject.transform.position = lostObjectSpawnPoint.position;

        spawnedGameObject.GetComponent<LostObject>().SetConveyorBelt(this);

        conveyorBeltObjects.Add(new ConveyorBeltObject(spawnedGameObject, 0));
    }

    public void RemoveConveyorLostObject(GameObject lostObject)
    {
        int lostObjectIndex = 0;
        bool found = false;
        while (!found && lostObjectIndex < conveyorBeltObjects.Count)
        {
            if (conveyorBeltObjects[lostObjectIndex].lostObject == lostObject)
            {
                found = true;
            }
            else
            {
                ++lostObjectIndex;
            }
        }
        if (lostObjectIndex == conveyorBeltObjects.Count)
        {
            Debug.Log("WHAAATTT IMPOSSIBLE!");
        }
        else
        {
            conveyorBeltObjects.RemoveAt(lostObjectIndex);
        }
    }
}
