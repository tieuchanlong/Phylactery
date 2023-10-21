using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BaseNode
{
    public SelectorNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {

    }

    public override NodeStatus Update(float fDeltaTime)
    {
        NodeStatus status;

        foreach (BaseNode child in _childrenNodes)
        {
            if (child.Update(fDeltaTime) == NodeStatus.Running)
            {
                return NodeStatus.Running;
            }
            else if (child.Update(fDeltaTime) == NodeStatus.Success)
            {
                return NodeStatus.Success;
            }
        }

        return NodeStatus.Failure;
    }
}
