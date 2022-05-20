using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiomassCounter : MonoBehaviour
{

    private Text biomassText;
    [SerializeField]
    private float updateInterval = 0.3f;

    private void Awake()
    {
        biomassText = GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (biomassText)
        {
            InvokeRepeating("UpdateCounter", 0f, updateInterval);
        }

    }

    private void UpdateCounter()
    {
        biomassText.text = BiomassManager.instance.CurrentBiomass + " x";
    }

   
}
