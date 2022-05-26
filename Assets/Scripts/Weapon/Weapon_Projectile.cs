using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Projectile : Weapon
{
    [SerializeField]
    protected Projectile projectilePrefab;

    [SerializeField]
    [Range(10f,30f)]
    protected float impulseForce = 20f;

    protected override void Fire()
    {
        base.Fire();

        Projectile projectile = Instantiate(projectilePrefab, muzzle.position, Quaternion.identity);
        Vector2 velocity = spreadVector * impulseForce;
        projectile.Init(velocity, ATK);
    }

}
