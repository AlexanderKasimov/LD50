using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected GameObject target;

    private Animator animator;
  
    private SpriteRenderer srCharacter;

    [SerializeField]
    private float movementSpeed = 4f;

    private Rigidbody2D rb;

    protected Vector2 targetDirection;

    protected float timeSinceAttack = 0f;

    [SerializeField]
    private float attackCooldown = 1f;

    [SerializeField]
    private float attackRange = 1f;

    private bool isShouldMove = true;

    protected StatsModule statsModule;

    private void Awake() 
    {        
        rb = GetComponent<Rigidbody2D>();        
        statsModule = GetComponent<StatsModule>();
        srCharacter = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Find closest target if no target
        if (!target)
        {
            target = FindTarget();
            isShouldMove = false;
        }
        //update vector to target or null it - no movement
        if (target)
        {
            targetDirection = GenerateVectorToTarget();
            float distanceToTarget = Vector2.Distance(target.transform.position, transform.position);
            if (distanceToTarget <= attackRange)
            {
                isShouldMove = false;
                if (timeSinceAttack >= attackCooldown)
                {
                    Attack();
                }
            }
            else
            {
                isShouldMove = true;
            }
          
        }
        else
        {
            targetDirection = Vector2.zero;
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

        // Update timeSinceAttack
        timeSinceAttack += Time.deltaTime;


        //find target
        //check for threshold

        // if (target)
        // {
        //     float distance = Vector2.Distance(target.transform.position, transform.position);
        //     if (distance <= 3f)
        //     {

        //     }
        // }

    }

    private void FixedUpdate()
    {
        if (isShouldMove)
        {
            Vector2 movementVector = targetDirection * movementSpeed * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + movementVector);
        }

    }

    protected virtual Vector2 GenerateVectorToTarget()
    {
        Vector2 targetDirection = (target.transform.position - transform.position);
        targetDirection = new Vector2(targetDirection.x, 0f).normalized;
        return targetDirection;
    }

    protected virtual void Attack()
    {
        // Debug.Log("Attack");
        timeSinceAttack = 0f;
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

    private void OnDrawGizmos() {


    }


}
