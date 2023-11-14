using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCrystalControl : DestructibleObjectControl
{
    protected override void Interact()
    {
        // Heal 50% of player HP
        PhylacteryPlayerMovement player = FindObjectOfType<PhylacteryPlayerMovement>();
        player.AddHP(player.MaxHP * 0.5f);

        base.Interact();
    }
}
