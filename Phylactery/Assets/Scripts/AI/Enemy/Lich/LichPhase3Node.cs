using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase3Node : SequenceNode
{
    public LichPhase3Node(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new LichPhase3HealthCheckNode(aiControl, parentNode));
        AddChild(new LichPhase3AttackNode(aiControl, parentNode));
    }
}
