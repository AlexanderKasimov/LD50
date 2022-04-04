using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flying : Enemy
{
    [SerializeField]
    private Projectile projectilePrefab;
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     base.Update();
    // }

    protected override Vector2 GenerateVectorToTarget()
    {
        Vector2 targetDirection = (target.transform.position - transform.position).normalized;  
        return targetDirection;
    }

    protected override void Attack()
    {
        base.Attack();
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.Init(targetDirection, statsModule.ATK);
    }
}
