using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMoveAroundNode : BaseNode
{
    public ZombieMoveAroundNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        // Do navigating action

        return NodeStatus.Running;
    }
}
