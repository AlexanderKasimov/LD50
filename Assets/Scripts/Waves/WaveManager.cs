using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Singleton
    public static WaveManager instance;

    [SerializeField]
    private float groupSpawnRate = 15f;


    [SerializeField]
    private List<SerializedKeyValuePair<GameObject, float>> enemyWeights;

    private Dictionary<GameObject, float> enemyWeightsDictionary;

    private float[] enemyWeightsArray;

    [SerializeField]
    private ScriptableObjectAnimCurve enemyCountCurve;

    [SerializeField]
    private ScriptableObjectAnimCurve groupSizeCurve;


    //Serialized for testing - later remove attribute
    [SerializeField]
    private int enemiesLeftToSpawn = 0;

    //Serialized for testing - later remove attribute
    [SerializeField]
    private int currentGroupSize = 0;


    private void Awake()
    {
        instance = this;

        enemyWeightsDictionary = RandomFunctionLibrary.ConvertListToDictionary(enemyWeights);
        // foreach (var item in enemyWeightsDictionary)
        // {
        //     Debug.Log("Key:" + item.Key + " | Value:" + item.Value);
        // }
        enemyWeightsArray = new float[enemyWeightsDictionary.Count];
        enemyWeightsDictionary.Values.CopyTo(enemyWeightsArray, 0);
        // RandomFunctionLibrary.TestWeights(enemyWeightsDictionary, 10000);

    }




    public void StartWave()
    {
        int currentDay = GameManager.instance.currentDay;
        enemiesLeftToSpawn = (int)enemyCountCurve.animationCurve.Evaluate(currentDay);
        Debug.Log("enemiesLeftToSpawn:" + enemiesLeftToSpawn);
        currentGroupSize = (int)groupSizeCurve.animationCurve.Evaluate(currentDay);
        Debug.Log("currentGroupSize:" + currentGroupSize);
        InvokeRepeating("SpawnGroup", 0f, groupSpawnRate);   
    }

    private void SpawnGroup()
    {
        // Debug.Log("SpawnGroup");
        enemiesLeftToSpawn -= currentGroupSize;
        if (enemiesLeftToSpawn <= 0)
        {
            EndWave();
        }
        for (int i = 0; i < currentGroupSize; i++)
        {
            int index = RandomFunctionLibrary.RandomIndexForWeights(enemyWeightsArray);
            // Debug.Log("index:" + index + " | item:" + enemyWeights[index].key);
            EnemySpawner enemySpawner = enemyWeights[index].key.GetComponent<EnemySpawner>();
            if (enemySpawner)
            {
                enemySpawner.SpawnEnemies(1);
            }
        }
    }

    private void EndWave()
    {
        Debug.Log("EndWave");
        CancelInvoke("SpawnGroup");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
