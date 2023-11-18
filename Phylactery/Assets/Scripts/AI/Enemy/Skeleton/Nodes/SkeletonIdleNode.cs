using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleNode : SequenceNode
{
    public SkeletonIdleNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new EnemyTakeDamageNode(aiControl, this));
        AddChild(new SkeletonCheckPlayerDistanceNode(aiControl, this));
        AddChild(new SkeletonMoveAroundNode(aiControl, this));
    }
}
