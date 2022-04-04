using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_Bonefire : Usable
{

    public override void Use()
    {
        GameManager.instance.StartEndDayEvent();
    }

}
