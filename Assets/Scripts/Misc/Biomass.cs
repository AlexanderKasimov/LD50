using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biomass : MonoBehaviour
{
    [SerializeField]
    private Vector2 dropCount = new Vector2(1, 5);

    private bool isCollided = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        //Probably don't possible to collide simultaniously in one frame with to different taged player objects - but still left for safety
        if (isCollided)
        {
            return;
        }
        isCollided = true;
        BiomassManager.instance.AddBiomass((int)Random.Range(dropCount.x, dropCount.y));
        Destroy(gameObject);
    }
}
