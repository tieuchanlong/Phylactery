using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenHealthNode : SequenceNode
{
    public CoachmenHealthNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new CoachmenHealthCheckNode(aiControl, this));
        AddChild(new CoachmenDieNode(aiControl, this));
    }
}
