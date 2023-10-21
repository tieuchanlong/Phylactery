using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieNode : SelectorNode
{
    public ZombieNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new ZombieHealthNode(aiControl, this));
        AddChild(new ZombieIdleNode(aiControl, this));
        AddChild(new ZombieAttackNode(aiControl, this));
    }
}
