using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    [SerializeField]
    private Color gridColor;

    [SerializeField]
    private int gridRows = 50;

    [SerializeField]
    private int gridColumns = 50;

    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;
        for (int i = 0; i < gridRows; i++)
        {          
            for (int j = 0; j < gridColumns; j++)
            {
                Vector2 pos = new Vector2(transform.position.x + j - gridColumns/2, transform.position.y - i + gridRows/2);
                Gizmos.DrawWireCube(pos, Vector3.one);
            }
        }
    }

}
