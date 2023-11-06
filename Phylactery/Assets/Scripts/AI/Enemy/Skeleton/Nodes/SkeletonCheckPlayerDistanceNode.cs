using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonCheckPlayerDistanceNode : BaseNode
{
    public SkeletonCheckPlayerDistanceNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        SkeletonControl skeletonControl = (SkeletonControl)_aiControl;

        if (!skeletonControl.IsInPlayerRange())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
