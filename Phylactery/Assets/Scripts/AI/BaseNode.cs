using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode
{
    public enum NodeStatus
    {
        Running,
        Success,
        Failure
    }

    protected BaseNode _parentNode;
    protected NodeStatus _nodeStatus;
    protected AIControl _aiControl;
    protected List<BaseNode> _childrenNodes;

    public BaseNode(AIControl aiControl, BaseNode parentNode)
    {
        _aiControl = aiControl;
        _parentNode = parentNode;
        _childrenNodes = new List<BaseNode>();
    }

    public virtual NodeStatus Update(float fDeltaTime)
    {
        return Execute(fDeltaTime);
    }

    protected virtual NodeStatus Execute(float fDeltaTime)
    {
        return NodeStatus.Success;
    }

    public void SetAIControl(AIControl aiControl)
    {
        _aiControl = aiControl;
    }

    public void SetParentNode(BaseNode parentNode)
    {
        _parentNode = parentNode;
    }

    public void AddChild(BaseNode child)
    {
        child.SetAIControl(_aiControl);
        child.SetParentNode(this);
        _childrenNodes.Add(child);
    }
}
