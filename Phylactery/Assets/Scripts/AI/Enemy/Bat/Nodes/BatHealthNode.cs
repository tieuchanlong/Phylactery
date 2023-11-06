using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatHealthNode : SequenceNode
{
    public BatHealthNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new BatHealthCheckNode(aiControl, this));
        AddChild(new BatDieNode(aiControl, this));
    }
}
