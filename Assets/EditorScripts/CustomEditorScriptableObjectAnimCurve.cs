using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptableObjectAnimCurve))]
public class CustomEditorScriptableObjectAnimCurve : Editor
{
    Vector2 scrollPosition;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScriptableObjectAnimCurve scriptableObjectAnimCurve = (ScriptableObjectAnimCurve)target; 
        AnimationCurve animationCurve = scriptableObjectAnimCurve.animationCurve;     

        if (animationCurve.length > 0)
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Key");
            EditorGUILayout.LabelField("Value");
            EditorGUILayout.EndHorizontal();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.MaxHeight(300));
            for (int i = 1; i <= (int)animationCurve.keys[animationCurve.keys.Length - 1].time; i++)
            {
                EditorGUILayout.BeginHorizontal("Box");
                EditorGUILayout.LabelField(i.ToString());
                EditorGUILayout.LabelField(((int)animationCurve.Evaluate(i)).ToString());
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }
    }

}
