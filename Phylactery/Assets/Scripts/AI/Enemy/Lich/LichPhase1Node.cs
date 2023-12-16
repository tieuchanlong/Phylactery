using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichPhase1Node : SequenceNode
{
    public LichPhase1Node(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new LichPhase1HealthCheckNode(aiControl, parentNode));
        AddChild(new LichPhase1AttackNode(aiControl, parentNode));
    }
}
