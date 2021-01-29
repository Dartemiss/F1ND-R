using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostObjectFactory : MonoBehaviour
{
    public static LostObjectFactory instance = null;

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

    public List<GameObject> lostObjectPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateLostObject(LostObject.LostObjectType type)
    {
        int lostObjectIndex = (int)type;
        GameObject lostObjectPrefab = lostObjectPrefabs[lostObjectIndex];
        return Instantiate(lostObjectPrefab);
    }
}
