using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCheckPlayerDistanceNode : BaseNode
{
    public BatCheckPlayerDistanceNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    protected override NodeStatus Execute(float fDeltaTime)
    {
        BatControl batControl = (BatControl)_aiControl;

        if (!batControl.IsInPlayerRange())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }
}
