using System.Collections;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    protected Character character;

    protected virtual void Awake()
    {    
        character = GetComponent<Character>();
    }

}
