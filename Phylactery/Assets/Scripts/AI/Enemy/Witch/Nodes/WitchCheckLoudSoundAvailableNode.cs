using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCheckLoudSoundAvailableNode : BaseNode
{
    public WitchCheckLoudSoundAvailableNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        WitchControl witchControl = (WitchControl)_aiControl;

        if (witchControl.IsInPlayerRange())
        {
            return NodeStatus.Failure;
        }

        // If there are no loud noises, the witch can continue idle state
        if (LoudNoiseLocation.LoudNoiseLocations.Count == 0)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
