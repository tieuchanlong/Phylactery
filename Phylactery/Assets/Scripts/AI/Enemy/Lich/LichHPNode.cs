using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichHPNode : SequenceNode
{
    public LichHPNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new LichHealthCheckNode(aiControl, this));
        AddChild(new LichDieNode(aiControl, this));
    }
}
