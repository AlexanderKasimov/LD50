using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotateVector : MonoBehaviour
{

    public Transform startingTransform;

    public float rotation = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        if (!startingTransform)
        {
            return;
        }
        Vector2 startingVector = startingTransform.right;
        Vector2 rotatedVector = Quaternion.Euler(0f, 0f, rotation) * startingVector;      
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + startingVector * 10f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + rotatedVector * 10f);


    }
}
