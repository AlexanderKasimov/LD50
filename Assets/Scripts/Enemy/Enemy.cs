using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject target;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            target = FindTarget();
        }


        // if (target)
        // {
        //     float distance = Vector2.Distance(target.transform.position, transform.position);
        //     if (distance <= 3f)
        //     {
               
        //     }
        // }




        //find target
        //check for threshold
        
    }

    private GameObject FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = float.MaxValue;
        GameObject closestTarget = null;
        foreach (var item in targets)
        {
            //Check for alive in DamageHandler or where it's stored
            // item.GetComponent<>
            float distance = Vector2.Distance(item.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = item;
            }
        }
        return closestTarget;
        
    }
}
