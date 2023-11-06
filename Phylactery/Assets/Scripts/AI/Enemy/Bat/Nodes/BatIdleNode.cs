using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIdleNode : SequenceNode
{
    public BatIdleNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new BatCheckPlayerDistanceNode(aiControl, this));
        AddChild(new BatMoveAroundNode(aiControl, this));
    }
}
