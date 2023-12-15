using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichHealthCheckNode : BaseNode
{
    public LichHealthCheckNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        LichBossControl lichBossControl = (LichBossControl)_aiControl;

        if (lichBossControl.HP <= 0)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
