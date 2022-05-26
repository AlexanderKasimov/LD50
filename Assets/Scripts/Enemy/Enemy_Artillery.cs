using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Artillery : Enemy
{
    [SerializeField]
    private Projectile projectilePrefab;
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
        float force = Mathf.Sqrt(x * x * (9.81f) / (x * Mathf.Sin(2 * angle) - (2 * y * Mathf.Cos(angle) * Mathf.Cos(angle))));
        // Debug.Log(force);
        //Try remove random - to fix Nan force
        if (float.IsNaN(force))
        {
            x = target.transform.position.x - transform.position.x;
            y = target.transform.position.y - transform.position.y;
            x = Mathf.Abs(x);
            y = Mathf.Abs(y);            
            force = Mathf.Sqrt(x * x * (9.81f) / (x * Mathf.Sin(2 * angle) - (2 * y * Mathf.Cos(angle) * Mathf.Cos(angle))));
            // Debug.Log(force);          
        }
        //Don't launch if force still Nan or launch with default - or even normal projectile
        if (!float.IsNaN(force))
        {         
            Vector2 launchVector = Quaternion.AngleAxis(Mathf.Sign(targetDirection.x) * launchAnlge, Vector3.forward) * targetDirection;
            Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector2 velocity = launchVector * force;
            projectile.Init(velocity, statsModule.ATK);
        }
     
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 launchVector = Quaternion.AngleAxis(Mathf.Sign(targetDirection.x) * launchAnlge, Vector3.forward) * targetDirection;   
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + launchVector);        

    }

}
