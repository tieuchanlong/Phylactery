using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenCheckPlayerDistanceNode : BaseNode
{
    public CoachmenCheckPlayerDistanceNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        CoachmenControl coachmenControl = (CoachmenControl)_aiControl;

        if (!coachmenControl.IsInPlayerRange())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
