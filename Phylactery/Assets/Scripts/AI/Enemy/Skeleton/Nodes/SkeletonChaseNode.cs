using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChaseNode : BaseNode
{
    public SkeletonChaseNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        SkeletonControl skeletonControl = (SkeletonControl)_aiControl;
        if (skeletonControl.ChasePlayer())
        {
            return NodeStatus.Success;
        }

        // If out side player range, don't chase, go back to idle
        if (!skeletonControl.IsInPlayerRange())
        {
            return NodeStatus.Failure;
        }

        // Do chasing animation
        skeletonControl.MoveToDestination(true);
        return NodeStatus.Running;
    }
}
