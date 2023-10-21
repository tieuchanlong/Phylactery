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
        // Do chasing animation
        return NodeStatus.Running;
    }
}
