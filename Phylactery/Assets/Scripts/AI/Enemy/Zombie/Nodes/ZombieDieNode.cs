using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDieNode : BaseNode
{
    public ZombieDieNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        // Do death animation
        return NodeStatus.Success;
    }
}
