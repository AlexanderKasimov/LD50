using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Projectile : Weapon
{
    [SerializeField]
    protected Projectile projectilePrefab;

    protected override void Fire()
    {
        base.Fire();
        Projectile projectile = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
        projectile.Init(spreadVector, ATK);
    }

}
