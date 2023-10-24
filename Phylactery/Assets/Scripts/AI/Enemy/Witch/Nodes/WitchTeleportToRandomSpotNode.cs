using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchTeleportToRandomSpotNode : BaseNode
{
    public WitchTeleportToRandomSpotNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        WitchControl _witchControl = (WitchControl)_aiControl;

        if (_witchControl.TeleportToRandomSpot())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
