using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class RandomFunctionLibrary
{
    
    #region Random weights, arrays
    //Choose random index from array of weights - alg from unity docs
    //https://docs.unity3d.com/2021.2/Documentation/Manual/class-Random.html

    public static int RandomIndexForWeights(float[] weights)
    {
        float sum = ArraySum(weights);
        float randomPoint = Random.value * sum;

        for (int i = 0; i < weights.Length; i++)
        {
            if (randomPoint < weights[i])
            {
                return i;
            }
            else
            {
                randomPoint -= weights[i];
            }
        }
        return weights.Length - 1;
    }
    //Test for RandomIndexForWeights

    public static void TestWeights<TKey>(Dictionary<TKey, float> dictionary, int numberOfTests)
    {
        if (dictionary.Count <= 0)
        {
            return;
        }
        int[] counter = new int[dictionary.Count];
        float[] weights = new float[dictionary.Count];

        dictionary.Values.CopyTo(weights, 0);
        for (int i = 0; i < numberOfTests; i++)
        {
            counter[RandomIndexForWeights(weights)]++;
        }
        //Count sums
        float sumWeights = ArraySum(weights);
        int sumCounter = ArraySum(counter);
        Debug.Log("Tests:" + numberOfTests + " | " + "Total Items:" + sumCounter);
        for (int i = 0; i < dictionary.Count; i++)
        {
            string itemDebugInfo = dictionary.ElementAt(i).Key + ":";
            itemDebugInfo += counter[i] + " items" + " | ";
            itemDebugInfo += ((float)counter[i] / (float)sumCounter) * 100f + "% ofCounter" + " | ";
            itemDebugInfo += weights[i]/sumWeights * 100f + " %ofWeights";
            Debug.Log(itemDebugInfo);
        }

    }

    public static Dictionary<TKey, TValue> ConvertListToDictionary<TKey, TValue>(List<SerializedKeyValuePair<TKey, TValue>> list)
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        foreach (var item in list)
        {
            dictionary[item.key] = item.value;
        }
        return dictionary;
    }
    // Array elements sums

    public static float ArraySum(float[] array)
    {
        float sum = 0f;
        foreach (var item in array)
        {
            sum += item;
        }
        return sum;
    }

    public static int ArraySum(int[] array)
    {
        int sum = 0;
        foreach (var item in array)
        {
            sum += item;
        }
        return sum;
    }

    #endregion

    public static Vector2 RandomVectorInCone(Vector2 coneDirection, float coneAngle)
    {
        float angle = Random.Range(-coneAngle / 2, coneAngle / 2);
        Vector2 resultVector = Quaternion.Euler(0f, 0f, angle) * coneDirection;
        return resultVector.normalized;
    }
}
