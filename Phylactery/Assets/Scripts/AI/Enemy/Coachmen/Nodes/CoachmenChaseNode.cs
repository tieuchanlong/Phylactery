using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenChaseNode : BaseNode
{
    public CoachmenChaseNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        CoachmenControl coachmenControl = (CoachmenControl)_aiControl;

        if (coachmenControl.IsAttackingPlayer())
        {
            return NodeStatus.Success;
        }

        if (coachmenControl.ChasePlayer())
        {
            return NodeStatus.Success;
        }

        // If out side player range, don't chase, go back to idle
        if (!coachmenControl.IsInPlayerRange())
        {
            return NodeStatus.Failure;
        }

        // Do chasing animation
        coachmenControl.MoveToDestination(true);
        return NodeStatus.Running;
    }
}
