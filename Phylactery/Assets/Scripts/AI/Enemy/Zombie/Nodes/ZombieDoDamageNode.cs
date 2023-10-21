using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDoDamageNode : BaseNode
{
    public ZombieDoDamageNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        // Do attack animation
        return NodeStatus.Running;
    }
}
