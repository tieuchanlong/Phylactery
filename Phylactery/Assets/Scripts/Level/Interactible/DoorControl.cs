using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : DestructibleObjectControl
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "StonePebble")
        {
            ProjectTileControl projectTile = collision.GetComponent<ProjectTileControl>();
            projectTile.ReduceHP();
            Destroy(gameObject);
        }
    }
}
