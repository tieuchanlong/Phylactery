using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDoAttackActionNode : BaseNode
{
    public WitchDoAttackActionNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        return NodeStatus.Running;
    }
}
