using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLD_Projectile : MonoBehaviour
{
    protected Vector2 movementDirection;

    protected float damage;

    private bool isCollided = false;
    [SerializeField]
    private float movementSpeed = 20f;

 

    [SerializeField]
    private List<string> layersToCollide;

    protected Rigidbody2D rb;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();     

    }

    // Start is called before the first frame update
    void Start()
    {
   
    }

    public void Init(Vector2 movementDirection, float damage)
    {
        this.movementDirection = movementDirection;
        this.damage = damage;
        //rotate towards movementDirection
        transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, movementDirection));
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        Vector2 movementVector = movementDirection * movementSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movementVector);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!layersToCollide.Contains(LayerMask.LayerToName(other.gameObject.layer)))
        {
            return;
        }
        //Fix simultaneous collisions - only register first
        if (isCollided)
        {
            return;
        }
        isCollided = true;
        OnCollided(other);
    }

    protected virtual void OnCollided(Collider2D other)
    {
        StatsModule statsModule = other.GetComponent<StatsModule>();
        if (statsModule)
        {
            statsModule.HandleDamage(damage);       
        }
        Destroy(gameObject);
    }

}
