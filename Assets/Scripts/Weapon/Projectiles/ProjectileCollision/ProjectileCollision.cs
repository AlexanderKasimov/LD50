using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileCollision : MonoBehaviour
{
    private bool isCollided = false;

    [SerializeField]
    protected List<string> layersToCollide;

    [SerializeField]
    protected int maxBounceCount = 0;

    protected int curBounceCount = 0;

    protected float damage = 0f;
       


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(float damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        curBounceCount++;
        if (curBounceCount <= maxBounceCount)
        {
            return;
        }
        Destroy(gameObject);
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

    protected abstract void OnCollided(Collider2D other);
   
}
