using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanCamera))]
[CanEditMultipleObjects]
public class CustomPlanCamera : Editor
{
    public SerializedProperty myTarget;

    public void OnEnable()
    {
        myTarget = serializedObject.FindProperty("activeGameObject");   
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        PlanCamera plan = (PlanCamera)target;
        GUILayout.Space(10);

        Debug.Log(myTarget);
        myTarget.arraySize = EditorGUILayout.IntField("Test 1", myTarget.arraySize);


        if (EditorGUILayout.PropertyField(myTarget, true))
        {
           


        }
        GUILayout.Space(10);
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Test"))
        {
            plan.Addition();
        }
    }
}
