using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    private List<SerializedKeyValuePair<GameObject, float>> itemsWeights;

    private Dictionary<GameObject, float> itemsWeightsDictionary;

    private float[] itemsWeigtsArray;

    private Vector2 randomRotation = new Vector2(0f, 360f);


    private void Awake()
    {
        //Init random wights system
        if (itemsWeights.Count > 0)
        {
            itemsWeightsDictionary = RandomFunctionLibrary.ConvertListToDictionary(itemsWeights);
            itemsWeigtsArray = new float[itemsWeightsDictionary.Count];
            itemsWeightsDictionary.Values.CopyTo(itemsWeigtsArray, 0);
            // RandomFunctionLibrary.TestWeights(itemsWeightsDictionary, 10000);    
        }
        else
        {
            Debug.Log("itemsWeights don't set");
        }    
    
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Drop()
    {
        if (itemsWeights.Count <= 0)
        {
            Debug.Log("itemsWeights don't set");
            return;
        }
        int randomIndex = RandomFunctionLibrary.RandomIndexForWeights(itemsWeigtsArray);        
        GameObject item = itemsWeights[randomIndex].key;
        if (item)
        {
            Instantiate(item, transform.position, Quaternion.Euler(0f, 0f, Random.Range(randomRotation.x, randomRotation.y)));
        }
      
    }

}
