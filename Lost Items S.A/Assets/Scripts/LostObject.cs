using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostObject : MonoBehaviour
{
    public enum LostObjectType
    {
        FOO_1,
        FOO_2,
        FOO_3
    }

    public LostObjectType lostObjectType;

    ConveyorBelt conveyorBelt = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetConveyorBelt(ConveyorBelt conveyorBelt)
    {
        this.conveyorBelt = conveyorBelt;
    }

    public void RemoveFromConveyorBelt()
    {
        this.conveyorBelt = null;
    }

    public bool IsInConveyorBelt()
    {
        return conveyorBelt != null;
    }
}
