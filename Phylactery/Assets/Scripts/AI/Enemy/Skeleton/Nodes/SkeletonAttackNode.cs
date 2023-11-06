using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackNode : SequenceNode
{
    public SkeletonAttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new SkeletonChaseNode(aiControl, this));
        AddChild(new SkeletonDashNode(aiControl, this));
    }
}
