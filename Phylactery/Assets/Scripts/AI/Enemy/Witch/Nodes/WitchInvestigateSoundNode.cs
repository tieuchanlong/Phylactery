using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchInvestigateSoundNode : BaseNode
{
    public WitchInvestigateSoundNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
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

        _witchControl.FinishInvestigateNoise();
        return NodeStatus.Success;
    }
}
