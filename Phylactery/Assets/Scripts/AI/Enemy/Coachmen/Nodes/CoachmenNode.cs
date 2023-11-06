using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoachmenNode : SelectorNode
{
    public CoachmenNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new CoachmenHealthNode(aiControl, this));
        AddChild(new CoachmenIdleNode(aiControl, this));
        AddChild(new CoachmenAttackNode(aiControl, this));
    }
}
