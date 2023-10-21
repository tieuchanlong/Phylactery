using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchFindRandomSpotNode : BaseNode
{
    public WitchFindRandomSpotNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        // Find random node on map to investigate
        return NodeStatus.Running;
    }
}
