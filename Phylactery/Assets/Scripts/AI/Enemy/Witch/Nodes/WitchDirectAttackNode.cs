using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchDirectAttackNode : SequenceNode
{
    public WitchDirectAttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new WitchCheckPlayerRangeNode(aiControl, this));
        AddChild(new WitchDoAttackActionNode(aiControl, this));
    }
}
