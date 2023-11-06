using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatChaseNode : BaseNode
{
    public BatChaseNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        BatControl batControl = (BatControl)_aiControl;
        if (batControl.ChasePlayer())
        {
            return NodeStatus.Success;
        }

        // If out side player range, don't chase, go back to idle
        if (!batControl.IsInPlayerRange())
        {
            return NodeStatus.Failure;
        }

        // Do chasing animation
        batControl.MoveToDestination(true);
        return NodeStatus.Running;
    }
}
