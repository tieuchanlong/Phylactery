using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchFindLoudSoundNode : BaseNode
{
    public WitchFindLoudSoundNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        WitchControl _witchControl = (WitchControl)_aiControl;

        if (_witchControl.IsInPlayerRange())
        {
            return NodeStatus.Failure;
        }

        if (_witchControl.FindLoudSound())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
