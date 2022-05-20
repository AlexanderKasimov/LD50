using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahController : CharacterController
{  
    
    [SerializeField]
    private Vector2 defaultDirection = Vector2.left;    

    [SerializeField]
    [Range(0f,15f)]
    private float maxTargetDistance = 10f;

    //Debug
    [Header("Debug")]
    [SerializeField]
    private bool debugEnabled = false;

    //Private
    private GameObject target;

    private Vector2 targetDirection;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            //Update target direction
            targetDirection = (target.transform.position - transform.position).normalized;
            //Fire when can
            character.WeaponStartFire();       
        }
        else
        {
            //find target
            target = FindTarget();
            //set target dir to default - to look at by default
            targetDirection = defaultDirection;
            character.WeaponStopFire();    
        }

        character.UpdateCharacter(targetDirection, Vector2.zero);
    }

    private GameObject FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");   
        float minDistance = maxTargetDistance;
        GameObject closestTarget = null;
        foreach (var item in targets)
        {
            float distance = Vector2.Distance(item.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = item;
            }
        }
        return closestTarget;

    }

    private void OnDrawGizmosSelected() 
    {
        if (!debugEnabled)
        {
            return;
        }
        //Debug maxTargetDistance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxTargetDistance);
    }


}
