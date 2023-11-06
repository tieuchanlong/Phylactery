using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatNode : SelectorNode
{
    public BatNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new BatHealthNode(aiControl, this));
        AddChild(new BatIdleNode(aiControl, this));
        AddChild(new BatAttackNode(aiControl, this));
    }
}
