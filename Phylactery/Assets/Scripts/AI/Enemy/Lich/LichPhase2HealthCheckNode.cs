using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase2HealthCheckNode : BaseNode
{
    public LichPhase2HealthCheckNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        LichBossControl lichBossControl = (LichBossControl)_aiControl;

        if (lichBossControl.HPRatio > 0.25f && lichBossControl.HPRatio <= 0.5f)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
