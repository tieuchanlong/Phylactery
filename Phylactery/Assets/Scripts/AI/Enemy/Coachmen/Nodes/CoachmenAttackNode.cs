using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenAttackNode : SequenceNode
{
    public CoachmenAttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new CoachmenChaseNode(aiControl, this));
        AddChild(new CoachmenDoAttackActionNode(aiControl, this));
    }
}
