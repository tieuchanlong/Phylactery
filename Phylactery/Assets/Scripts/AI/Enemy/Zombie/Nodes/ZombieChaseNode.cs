using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieChaseNode : BaseNode
{
    public ZombieChaseNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        ZombieControl zombieControl = (ZombieControl)_aiControl;
        if (zombieControl.ChasePlayer())
        {
            return NodeStatus.Success;
        }

        // If out side player range, don't chase, go back to idle
        if (!zombieControl.IsInPlayerRange())
        {
            return NodeStatus.Failure;
        }

        // Do chasing animation
        zombieControl.MoveToDestination();
        return NodeStatus.Running;
    }
}
