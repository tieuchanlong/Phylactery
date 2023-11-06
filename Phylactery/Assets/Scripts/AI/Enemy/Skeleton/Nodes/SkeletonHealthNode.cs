using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealthNode : SequenceNode
{
    public SkeletonHealthNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new SkeletonHealthCheckNode(aiControl, this));
        AddChild(new SkeletonDieNode(aiControl, this));
    }
}
