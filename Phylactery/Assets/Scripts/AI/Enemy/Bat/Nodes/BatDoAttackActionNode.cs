using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatDoAttackActionNode : BaseNode
{
    public BatDoAttackActionNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        BatControl batControl = (BatControl)_aiControl;

        // Do attack animation
        if (batControl.DoAttackAction())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Running;
    }
}
