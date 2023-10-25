using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchIdleNode : SequenceNode
{
    public WitchIdleNode(AIControl aiControl, BaseNode parentNode) : base(aiControl, parentNode)
    {
        AddChild(new WitchCheckLoudSoundAvailableNode(aiControl, this));
        AddChild(new WitchTeleportToRandomSpotNode(aiControl, this));
        AddChild(new WitchFindRandomSpotNode(aiControl, this));
        AddChild(new WitchInvestigateRandomSpotNode(aiControl, this));
    }
}
