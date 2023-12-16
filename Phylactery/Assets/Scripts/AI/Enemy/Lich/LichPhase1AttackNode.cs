using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase1AttackNode : BaseNode
{
    public LichPhase1AttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        LichBossControl lichBossControl = (LichBossControl)_aiControl;
        lichBossControl.DoAttackPhase1();

        return NodeStatus.Running;
    }
}
