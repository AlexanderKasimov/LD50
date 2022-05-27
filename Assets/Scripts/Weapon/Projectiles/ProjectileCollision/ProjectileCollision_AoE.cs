using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision_AoE : ProjectileCollision
{
    [SerializeField]
    protected float damageRadius = 1f;

    private bool isExploded = false;
    
    [SerializeField]
    private GameObject explosionVFX;

    protected override void OnCollided(Collider2D other)
    {
        Explode();
    }

    protected void OnDestroy()
    {   
        //Error if projectile was preplaced on scene (can remove later?)
        if (!gameObject.scene.isLoaded)
        {
            Debug.Log("Not loaded!");
            return;
        }
        Explode();        
    }

    protected void Explode()
    {      
        if (isExploded)
        {
            return;
        }
        isExploded = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, damageRadius, LayerMask.GetMask(layersToCollide.ToArray()));
        foreach (var collider in colliders)
        {
            StatsModule statsModule = collider.GetComponent<StatsModule>();
            if (statsModule)
            {
                statsModule.HandleDamage(damage);
            }           
        }
        //Play VFX
        if (explosionVFX)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);         
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
