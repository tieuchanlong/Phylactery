using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase2Node : SequenceNode
{
    public LichPhase2Node(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new LichPhase2HealthCheckNode(aiControl, parentNode));
        AddChild(new LichPhase2AttackNode(aiControl, parentNode));
    }
}
