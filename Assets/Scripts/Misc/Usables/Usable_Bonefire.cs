using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_Bonefire : Usable
{

    public override void Use()
    {
        GameManager.instance.TransitionToBatte();
        //Debug.LogWarning("Bonefire not working yet!");
        //GameManager.instance.StartEndDayEvent();
    }

}
