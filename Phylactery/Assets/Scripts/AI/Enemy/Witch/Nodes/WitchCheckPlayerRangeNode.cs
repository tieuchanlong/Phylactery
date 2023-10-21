using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchCheckPlayerRangeNode : BaseNode
{
    public WitchCheckPlayerRangeNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        WitchControl witchControl = (WitchControl)_aiControl;

        if (!witchControl.IsInPlayerRange())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
