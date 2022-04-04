using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee : Enemy
{
    [SerializeField]
    private float attackPointDistance = 1f;

    [SerializeField]
    private Vector2 attackBoxSize = new Vector2(1f, 1f);

    protected override void Attack()
    {
        base.Attack();
        Vector2 attackPoint = (Vector2)transform.position + targetDirection * attackPointDistance;
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPoint, attackBoxSize, 0f, LayerMask.GetMask("Player"));
        foreach (var collider in colliders)
        {
            StatsModule statsModule = collider.gameObject.GetComponent<StatsModule>();
            if (statsModule)
            {
                statsModule.HandleDamage(this.statsModule.ATK);
            }
        }


    }

    private void OnDrawGizmosSelected()
    {
        Vector2 attackPoint = (Vector2)transform.position + targetDirection * attackPointDistance;
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(attackPoint, attackBoxSize);

    }
   
}
