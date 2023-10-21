using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackNode : SequenceNode
{
    public ZombieAttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new ZombieChaseNode(aiControl, this));
        AddChild(new ZombieDoDamageNode(aiControl, this));
    }
}
