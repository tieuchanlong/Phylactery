using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase3AttackNode : BaseNode
{
    public LichPhase3AttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        LichBossControl lichBossControl = (LichBossControl)_aiControl;
        lichBossControl.DoAttackPhase3();

        return NodeStatus.Running;
    }
}
