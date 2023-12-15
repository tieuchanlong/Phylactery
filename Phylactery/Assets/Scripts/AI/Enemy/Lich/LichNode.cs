using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichNode : SelectorNode
{
    public LichNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new LichHPNode(aiControl, this));
        AddChild(new LichPhase1Node(aiControl, this));
        AddChild(new LichPhase2Node(aiControl, this));
        AddChild(new LichPhase3Node(aiControl, this));
    }
}
