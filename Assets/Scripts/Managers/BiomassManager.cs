using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomassManager : MonoBehaviour
{
    //Singleton
    public static BiomassManager instance;

    public int currentBiomass = 0;


    private void Awake() 
    {
        instance = this;
    }

    public void AddBiomass(int value)
    {
        currentBiomass += value;
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
