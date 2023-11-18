using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCrystalControl : DestructibleObjectControl
{
    private GameControl _gameControl;

    protected override void Start()
    {
        _gameControl = FindObjectOfType<GameControl>();
        _destroyedAfterInteraction = false;
        base.Start();
    }

    protected override void Interact()
    {
        _gameControl.SaveGame();

        base.Interact();
    }
}
