using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BaseNode
{
    public SequenceNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
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
            else if (child.Update(fDeltaTime) == NodeStatus.Failure)
            {
                return NodeStatus.Failure;
            }
        }

        return NodeStatus.Success;
    }
}
