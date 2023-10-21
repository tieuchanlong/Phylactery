using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieCheckPlayerDistanceNode : BaseNode
{
    public ZombieCheckPlayerDistanceNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        ZombieControl zombieControl = (ZombieControl)_aiControl;

        if (!zombieControl.IsInPlayerRange())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
