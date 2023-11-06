using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealthCheckNode : BaseNode
{
    public SkeletonHealthCheckNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        SkeletonControl skeletonControl = (SkeletonControl)_aiControl;

        if (skeletonControl.HP <= 0)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
