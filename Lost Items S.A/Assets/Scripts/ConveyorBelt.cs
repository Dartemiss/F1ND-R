using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public Transform lostObjectSpawnPoint;
    public List<Transform> beltPath;
    public float beltSpeed;

    struct ConveyorBeltObject
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
    List<ConveyorBeltObject> conveyorBeltOjects;
    

    // Start is called before the first frame update
    void Start()
    {
        conveyorBeltOjects = new List<ConveyorBeltObject>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnLostObject(LostObject.LostObjectType.FOO_1);
        }

        for (int i = 0; i < conveyorBeltOjects.Count; ++i)
        {
            ConveyorBeltObject spawnedLostObject = conveyorBeltOjects[i];

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

            }
            else
            {
                float previousTimeStamp = beltTimeStamps[spawnedLostObject.currentTimeStampIndex];
                float nextTimeStamp = beltTimeStamps[spawnedLostObject.currentTimeStampIndex + 1];
                float progress = (spawnedLostObject.elapsedTime - previousTimeStamp) / (nextTimeStamp - previousTimeStamp);

                Vector3 previousBeltPosition = beltPath[spawnedLostObject.currentTimeStampIndex].position;
                Vector3 nextBeltPosition = beltPath[spawnedLostObject.currentTimeStampIndex + 1].position;
                spawnedLostObject.lostObject.transform.position = Vector3.Lerp(previousBeltPosition, nextBeltPosition, progress);

                conveyorBeltOjects[i] = spawnedLostObject;
            }
        }
    }

    public void SpawnLostObject(LostObject.LostObjectType lostObjectType)
    {
        GameObject spawnedGameObject = LostObjectFactory.instance.CreateLostObject(lostObjectType);
        spawnedGameObject.transform.position = lostObjectSpawnPoint.position;

        conveyorBeltOjects.Add(new ConveyorBeltObject(spawnedGameObject, 0));
        //Debug.Log("Hola");
    }
}
