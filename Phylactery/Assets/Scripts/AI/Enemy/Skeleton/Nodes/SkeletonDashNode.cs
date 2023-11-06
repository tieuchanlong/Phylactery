using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDashNode : BaseNode
{
    public SkeletonDashNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        SkeletonControl skeletonControl = (SkeletonControl)_aiControl;

        // Do attack animation
        if (skeletonControl.DoDashAction())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Running;
    }
}
