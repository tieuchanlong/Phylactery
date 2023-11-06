using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenDoAttackActionNode : BaseNode
{
    public CoachmenDoAttackActionNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        CoachmenControl coachmenControl = (CoachmenControl)_aiControl;

        // Do attack animation
        if (coachmenControl.DoAttackAction())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Running;
    }
}
