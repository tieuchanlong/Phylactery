using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthNode : SequenceNode
{
    public ZombieHealthNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new ZombieHealthCheckNode(aiControl, this));
        AddChild(new ZombieDieNode(aiControl, this));
    }
}
