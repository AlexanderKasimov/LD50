using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    protected Rigidbody2D rb;

    [SerializeField]
    protected GameObject artObject;

    protected ProjectileCollision projectileCollision;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileCollision = GetComponent<ProjectileCollision>();
    }

    public void Init(Vector2 velocity, float damage)
    {
        rb.AddForce(velocity, ForceMode2D.Impulse);
        projectileCollision.Init(damage);
        if (artObject)
        {
            artObject.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, velocity.normalized));
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (artObject)
        {
            artObject.transform.rotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, rb.velocity.normalized));
        }
    }




}
