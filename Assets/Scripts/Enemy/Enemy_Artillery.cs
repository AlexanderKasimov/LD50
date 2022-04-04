using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Artillery : Enemy
{
    [SerializeField]
    private PhysicalProjectile physicalProjectilePrefab;
    [SerializeField]
    private float launchAnlge = 30f;

    [SerializeField]
    private Vector2 angleRandom = new Vector2(-5f, 5f);

    [SerializeField]
    private Vector2 targetRandomVector = new Vector2(-1f, 1f);


    protected override void Attack()
    {
        base.Attack();
        float x = target.transform.position.x - transform.position.x + Random.Range(targetRandomVector.x, targetRandomVector.y);
        float y = target.transform.position.y - transform.position.y;
        x = Mathf.Abs(x);
        y = Mathf.Abs(y);
        float angle = Mathf.Deg2Rad * (launchAnlge + Random.Range(angleRandom.x, angleRandom.y));
        float velocity = Mathf.Sqrt(x * x * (9.81f) / (x * Mathf.Sin(2 * angle) - (2 * y * Mathf.Cos(angle) * Mathf.Cos(angle))));
        Debug.Log(velocity);
        //Try remove random - to fix Nan velocity
        if (float.IsNaN(velocity))
        {
            x = target.transform.position.x - transform.position.x;
            y = target.transform.position.y - transform.position.y;
            x = Mathf.Abs(x);
            y = Mathf.Abs(y);            
            velocity = Mathf.Sqrt(x * x * (9.81f) / (x * Mathf.Sin(2 * angle) - (2 * y * Mathf.Cos(angle) * Mathf.Cos(angle))));
            Debug.Log(velocity);          
        }
        //Don't launch if velocity still Nan or launch with default - or even normal projectile
        if (!float.IsNaN(velocity))
        {
            Vector2 launchVector = Quaternion.AngleAxis(Mathf.Sign(targetDirection.x) * launchAnlge, Vector3.forward) * targetDirection;
            PhysicalProjectile projectile = Instantiate(physicalProjectilePrefab, transform.position, Quaternion.identity);
            projectile.Init(launchVector, statsModule.ATK, velocity);
        }

     
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 launchVector = Quaternion.AngleAxis(Mathf.Sign(targetDirection.x) * launchAnlge, Vector3.forward) * targetDirection;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + launchVector);

    }

}
