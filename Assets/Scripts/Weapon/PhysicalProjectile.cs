using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : MonoBehaviour
{


    private Vector2 movementDirection;

    private float damage;

    private bool isCollided = false;

    private Rigidbody2D rb;

    [SerializeField]
    private List<string> layersToCollide;  

    [SerializeField]
    private GameObject projectileArt;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (projectileArt)
        {
            projectileArt.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, rb.velocity.normalized));
        }   
    }

    private void FixedUpdate()
    {

    }

    public void Init(Vector2 movementDirection, float damage, float velocity)
    {
        this.movementDirection = movementDirection;
        this.damage = damage;
        rb.AddForce(movementDirection * velocity, ForceMode2D.Impulse);
        //rotate towards movementDirection
        // transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, movementDirection));
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
        // Debug.Log(LayerMask.LayerToName(other.gameObject.layer));
        StatsModule statsModule = other.GetComponent<StatsModule>();
        if (statsModule)
        {
            statsModule.HandleDamage(damage);
            // Debug.Log(statsModule.curHP);
        }
        Destroy(gameObject);
    }

}
