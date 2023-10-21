using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchInvestigateSoundNode : BaseNode
{
    public WitchInvestigateSoundNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        return NodeStatus.Running;
    }
}
