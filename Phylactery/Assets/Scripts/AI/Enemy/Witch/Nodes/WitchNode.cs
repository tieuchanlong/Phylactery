using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchNode : SelectorNode
{
    public WitchNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new WitchIdleNode(aiControl, this));
        AddChild(new WitchAttackNode(aiControl, this));
    }
}
