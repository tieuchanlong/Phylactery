using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenIdleNode : SequenceNode
{
    public CoachmenIdleNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new CoachmenCheckPlayerDistanceNode(aiControl, this));
        AddChild(new CoachmenMoveAroundNode(aiControl, this));
    }
}
