using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase3HealthCheckNode : BaseNode
{
    public LichPhase3HealthCheckNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        LichBossControl lichBossControl = (LichBossControl)_aiControl;

        if (lichBossControl.HPRatio > 0.0f && lichBossControl.HPRatio <= 0.25f)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
