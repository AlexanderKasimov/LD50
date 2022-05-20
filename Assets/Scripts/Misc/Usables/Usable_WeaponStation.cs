using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_WeaponStation : Usable
{

    public override void Use()
    {
        WeaponStationManager.instance.Open();        
    }


}
