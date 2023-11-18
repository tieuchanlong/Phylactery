using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamageNode : BaseNode
{
    public EnemyTakeDamageNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        // Check for Axe Attack range
        if (!_aiControl.IsInPlayerAttackRange())
        {
            return NodeStatus.Success;
        }

        // Do AI damage animation
        _aiControl.TakeDamage(1.0f);

        if (_aiControl.IsTakingDamage())
        {
            return NodeStatus.Running;
        }

        return NodeStatus.Failure;
    }
}
