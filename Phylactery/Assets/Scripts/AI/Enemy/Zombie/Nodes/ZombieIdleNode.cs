using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleNode : SequenceNode
{
    public ZombieIdleNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new EnemyTakeDamageNode(aiControl, this));
        AddChild(new ZombieCheckPlayerDistanceNode(aiControl, this));
        AddChild(new ZombieMoveAroundNode(aiControl, this));
    }
}
