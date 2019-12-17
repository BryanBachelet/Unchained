using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraAction))]
public class CustomCamAction : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CameraAction cameraAction = (CameraAction)target;
        cameraAction.player = GameObject.Find("Player");
        if (GUILayout.Button("Save Profil"))
        {
            cameraAction.ecartJoueur =  cameraAction.transform.position - cameraAction.player.transform.position;
            cameraAction.profil.position = cameraAction.ecartJoueur;
            cameraAction.profil.rotation = cameraAction.transform.eulerAngles;
        }
        if (GUILayout.Button("Aplie Profil"))
        {
            cameraAction.transform.position = cameraAction.player.transform.position +  cameraAction.profil.position;
            cameraAction.transform.eulerAngles = cameraAction.profil.rotation;  

        }
    }
}
