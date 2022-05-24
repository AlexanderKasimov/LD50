using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Laser : Weapon
{
    [Header("Laser")]
    [SerializeField]
    [Range(0f, 2f)]
    protected float laserHeight = 1f;

    [SerializeField]
    [Range(0f, 20f)]
    protected float laserDistance = 10f;

    [SerializeField]
    protected LaserBeam laserBeamPrefab;

    //Not really needed exposed to inspector
    [SerializeField]
    protected float traceBoxLength = 1f;

    //Testing stuff
    [SerializeField]
    protected SpawnBeam OLD_laserBeamPrefab;

    [SerializeField]
    private bool useOldBeam = false;

    //private 
    protected override void Fire()
    {
        base.Fire();
        
        float laserAngle = Vector2.SignedAngle(Vector2.right, spreadVector);
        RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(muzzle.position, new Vector2(traceBoxLength, laserHeight), laserAngle, spreadVector, laserDistance, LayerMask.GetMask("Enemy"));
        foreach (var raycastHit in raycastHits)
        {      
            StatsModule statsModule = raycastHit.collider.gameObject.GetComponent<StatsModule>();
            if (statsModule)
            {
                statsModule.HandleDamage(ATK);
            }
        }
        //Spawn beam
        //Old beam for testing
        if (useOldBeam)
        {
            //OLD beam
            SpawnBeam beam = Instantiate(OLD_laserBeamPrefab, muzzle.position, Quaternion.identity);
            beam.Init(muzzle.position, (Vector2)muzzle.position + spreadVector * laserDistance);
        }
        else
        {
            LaserBeam laserBeam = Instantiate(laserBeamPrefab, muzzle.position, Quaternion.identity);
            laserBeam.Init(muzzle.position, (Vector2)muzzle.position + spreadVector * (laserDistance + traceBoxLength/2));      
            
        }            

        //Debug
        //if (!isDebugEnabled)
        //{
        //    return;
        //}
        Vector2 pos = (Vector2)muzzle.position + spreadVector * (laserDistance / 2);
        DebugDraw.Box(pos, spreadVector, laserDistance + traceBoxLength, laserHeight, Color.red, 2f);

    }

}
