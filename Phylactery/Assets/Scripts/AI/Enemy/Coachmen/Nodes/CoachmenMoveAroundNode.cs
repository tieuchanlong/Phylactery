using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenMoveAroundNode : BaseNode
{
    public CoachmenMoveAroundNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        // Do navigating action
        return NodeStatus.Running;
    }
}
