using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDoDamageNode : BaseNode
{
    public ZombieDoDamageNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        ZombieControl zombieControl = (ZombieControl)_aiControl;

        // Do attack animation
        if (zombieControl.DoAttackAction())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Running;
    }
}
