using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDieNode : BaseNode
{
    public BatDieNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        // Do death animation
        return NodeStatus.Success;
    }
}
