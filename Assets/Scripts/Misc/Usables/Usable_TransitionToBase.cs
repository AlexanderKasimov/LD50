using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_TransitionToBase : Usable
{
    public override void Use()
    {
        GameManager.instance.TransitionToBase();
    }
    
}
