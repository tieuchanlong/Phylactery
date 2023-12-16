using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase2AttackNode : BaseNode
{
    public LichPhase2AttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        LichBossControl lichBossControl = (LichBossControl)_aiControl;
        lichBossControl.DoAttackPhase2();

        return NodeStatus.Running;
    }
}
