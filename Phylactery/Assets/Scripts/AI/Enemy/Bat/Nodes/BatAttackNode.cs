using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackNode : SequenceNode
{
    public BatAttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new BatChaseNode(aiControl, this));
        AddChild(new BatDoAttackActionNode(aiControl, this));
    }
}
