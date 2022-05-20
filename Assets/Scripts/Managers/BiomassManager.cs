using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomassManager : MonoBehaviour
{
    //Singleton
    public static BiomassManager instance;

    [field: SerializeField] public int CurrentBiomass { get; private set; } = 0;


    private void Awake() 
    {
        instance = this;
    }

    public void AddBiomass(int value)
    {
        CurrentBiomass += value;
    }

    public void SubtractBiomass(int value)
    {
        CurrentBiomass = Mathf.Max(0, CurrentBiomass - value);
    }

    public bool TryToPurchase(int price)
    {   
        if (CurrentBiomass < price)
        {
            Debug.Log("Purchase failed, not enough biomass:" + CurrentBiomass + "/" + price);
            return false;
        }
        SubtractBiomass(price);
        return true;
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
