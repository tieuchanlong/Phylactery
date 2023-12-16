using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase1HealthCheckNode : BaseNode
{
    public LichPhase1HealthCheckNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        LichBossControl lichBossControl = (LichBossControl)_aiControl;

        if (lichBossControl.HPRatio > 0.5f)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
