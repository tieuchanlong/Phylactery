using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAttackNode : SequenceNode
{
    public WitchAttackNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new WitchInvestigateSoundNode(aiControl, this));
        AddChild(new WitchDoAttackActionNode(aiControl, this));
    }
}
