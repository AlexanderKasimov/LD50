using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler_Enemy : DeathHandler
{
    public override void HandleDeath()
    {
        base.HandleDeath();
        DropItem dropItem = GetComponent<DropItem>();
        if (dropItem)
        {
            dropItem.Drop();
        }
    }
}
