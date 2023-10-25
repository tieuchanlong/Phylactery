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

        if (TeleportLocationControl.TeleportPoints.Count == 0)
        {
            return NodeStatus.Failure;
        }

        if (!_witchControl.TeleportToRandomSpot())
        {
            if (!_witchControl.FindNewRandomSpot)
            {
                return NodeStatus.Failure;
            }
            return NodeStatus.Running;
        }

        return NodeStatus.Success;
    }
}
