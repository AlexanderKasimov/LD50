using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision_Bullet : ProjectileCollision
{


    protected override void OnCollided(Collider2D other)
    {
        StatsModule statsModule = other.GetComponent<StatsModule>();
        if (statsModule)
        {
            statsModule.HandleDamage(damage);
        }
        Destroy(gameObject);
    }
}
