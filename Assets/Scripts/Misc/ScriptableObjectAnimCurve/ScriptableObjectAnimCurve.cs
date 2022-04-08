using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectAnimCurve_", menuName = "Scriptable Objects/AnimCurve")]
public class ScriptableObjectAnimCurve : ScriptableObject
{
    public AnimationCurve animationCurve = new AnimationCurve();

}
