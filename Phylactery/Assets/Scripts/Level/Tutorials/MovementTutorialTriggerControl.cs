using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorialTriggerControl : TutorialTriggerControl
{
    protected override bool IsUnlocked()
    {
        return _gameControl.IsMovementTutorialUnlocked;
    }

    public override void ActivateTutorial()
    {
        base.ActivateTutorial();
        _gameControl.IsMovementTutorialUnlocked = true;
    }
}
