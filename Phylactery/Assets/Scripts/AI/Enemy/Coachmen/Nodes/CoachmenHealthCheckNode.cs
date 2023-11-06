using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenHealthCheckNode : BaseNode
{
    public CoachmenHealthCheckNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        if (_aiControl.HP <= 0)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
