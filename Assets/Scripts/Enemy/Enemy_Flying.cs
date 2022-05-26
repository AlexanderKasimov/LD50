using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flying : Enemy
{
    [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField]
    protected float impusleForce = 15f;

    [SerializeField]
    protected float spreadAngle = 5f;


    protected override Vector2 GenerateVectorToTarget()
    {
        Vector2 targetDirection = (target.transform.position - transform.position).normalized;  
        return targetDirection;
    }

    protected override void Attack()
    {
        base.Attack();
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector2 spreadVector = RandomFunctionLibrary.RandomVectorInCone(targetDirection, spreadAngle);
        Vector2 velocity = spreadVector * impusleForce;
        projectile.Init(velocity, statsModule.ATK);
    }
}
