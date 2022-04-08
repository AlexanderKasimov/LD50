using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private Vector2 randomX;

    [SerializeField]
    private Vector2 randomY;

    public void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomOffset = new Vector2(Random.Range(randomX.x, randomX.y), Random.Range(randomY.x, randomY.y));
            Instantiate(enemyPrefab, (Vector2)transform.position + randomOffset, Quaternion.identity);
        }
    }

}
