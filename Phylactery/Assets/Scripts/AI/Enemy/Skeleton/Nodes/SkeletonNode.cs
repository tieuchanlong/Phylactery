using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonNode : SelectorNode
{
    public SkeletonNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new SkeletonHealthNode(aiControl, this));
        AddChild(new SkeletonIdleNode(aiControl, this));
        AddChild(new SkeletonAttackNode(aiControl, this));
    }
}
