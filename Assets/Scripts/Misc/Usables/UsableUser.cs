using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableUser : MonoBehaviour
{
    private Usable curUsable;

    public void AddUsable(Usable usable)
    {
        curUsable = usable;
    }

    public void RemoveUsable(Usable usable)
    {
        curUsable = null;
    }

    public void Use()
    {
        if (curUsable)
        {
            curUsable.Use();
        }
    }





}
