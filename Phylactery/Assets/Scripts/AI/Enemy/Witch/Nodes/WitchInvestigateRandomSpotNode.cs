using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchInvestigateRandomSpotNode : BaseNode
{
    public WitchInvestigateRandomSpotNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        WitchControl _witchControl = (WitchControl)_aiControl;

        if (!_witchControl.ReachedDestination())
        {
            _witchControl.MoveToDestination();
            return NodeStatus.Running;
        }

        return NodeStatus.Success;
    }
}
