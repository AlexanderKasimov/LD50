using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahController : MonoBehaviour
{
    //Singleton
    public static SarahController instance;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Animator animator;

    private Vector2 targetDirection;
    [SerializeField]
    private Vector2 defaultDirection = Vector2.left;

    public StatsModule statsModule;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private SpriteRenderer srCharacter;



    private void Awake()
    {
        instance = this;
        statsModule = GetComponent<StatsModule>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            //Update target direction
            targetDirection = (target.transform.position - transform.position).normalized;
            //Fire when can
            if (weapon)
            {
                weapon.StartFire();
            }
        }
        else
        {
            //find target
            target = FindTarget();
            //set target dir to default - to look at by default
            targetDirection = defaultDirection;
            if (weapon)
            {
                weapon.StopFire();
            }



        }

        if (weapon)
        {
            weapon.UpdateRotation(targetDirection);
        }
        //Rotate sprite    
        if (targetDirection.x <= 0)
        {
            srCharacter.flipX = true;
        }
        else
        {
            srCharacter.flipX = false;
        }

    }

    private GameObject FindTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        float minDistance = float.MaxValue;
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



}
