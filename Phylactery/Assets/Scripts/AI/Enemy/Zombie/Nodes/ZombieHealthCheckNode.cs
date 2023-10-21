using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthCheckNode : BaseNode
{
    public ZombieHealthCheckNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        ZombieControl zombieControl = (ZombieControl)_aiControl;
        
        if (zombieControl.HP <= 0)
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
