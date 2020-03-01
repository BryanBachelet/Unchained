using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class WindowScene : EditorWindow
{

    public string SceneName;
    public int index;
    SceneAsset sceneAdd;
    [SerializeField]
    SceneAsset sceneAssets;
    [SerializeField]
    SceneAsset sceneDuplicate;
    private DefaultAsset targetFolder = null;
    string targetFolderPath;
    private DefaultAsset FolderScenPrefab = null;
    string folderScenePrefabPath;



    [MenuItem("Tools/SceneInstantiate")]
    public static void ShowWindow()
    {
        GetWindow<WindowScene>("Scene Instance");
    }

    private void OnGUI()
    {

        GUILayout.Space(10);
        FolderScenPrefab = (DefaultAsset)EditorGUILayout.ObjectField("Folder Of Prefab Scene", FolderScenPrefab, typeof(DefaultAsset), false);

        if (FolderScenPrefab != null)
        {
            folderScenePrefabPath = AssetDatabase.GetAssetPath(FolderScenPrefab.GetInstanceID());

            if (!Directory.Exists(folderScenePrefabPath))
            {

                FolderScenPrefab = null;
            }


            GUILayout.Label("Scène par defaut", EditorStyles.boldLabel);
            string[] aMaterialFiles = Directory.GetFiles(folderScenePrefabPath + "/", "*.unity", SearchOption.AllDirectories);

            for (int i = 0; i < aMaterialFiles.Length; i++)
            {
                Object tryAsset = AssetDatabase.LoadAssetAtPath(aMaterialFiles[i], typeof(SceneAsset));
                sceneAssets = tryAsset as SceneAsset;
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.ObjectField(sceneAssets.name, sceneAssets, typeof(SceneAsset), true);
                if (sceneDuplicate == null)
                {
                    sceneDuplicate = sceneAssets;
                }
                if (GUILayout.Button("+"))
                {
                    sceneDuplicate = sceneAssets;
                }
                EditorGUILayout.EndHorizontal();
            }
            GUILayout.Space(10f);
            GUILayout.Label("Nouvelle Scene", EditorStyles.boldLabel);
            targetFolder = (DefaultAsset)EditorGUILayout.ObjectField("Select Folder", targetFolder, typeof(DefaultAsset), false);

            if (targetFolder != null)
            {
                targetFolderPath = AssetDatabase.GetAssetPath(targetFolder.GetInstanceID());

                if (!Directory.Exists(targetFolderPath))
                {

                    targetFolder = null;
                }
            }

            ActiveWarning(targetFolder, "Valid Folder", "Not Valid, Put a folder");



            EditorGUI.BeginDisabledGroup(targetFolder == null);

            sceneDuplicate = (SceneAsset)EditorGUILayout.ObjectField("Scene Instantiate", sceneDuplicate, typeof(SceneAsset), true);
            SceneName = EditorGUILayout.TextField("Scene's Name", SceneName);
            index = EditorGUILayout.IntField("Index", index);

            if (GUILayout.Button("Instantiate Scene"))
            {
                DuplicateScene();
            }
            EditorGUI.EndDisabledGroup();
        }
    }

    public void DuplicateScene()
    {

        bool save = AssetDatabase.CopyAsset("Assets/ScenePrefab/" + sceneDuplicate.name + ".unity", targetFolderPath + "/" + SceneName + index + ".unity");
        index++;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void ActiveWarning(Object objectVerifiy, string validMessage, string notValidMessage)
    {
        if (objectVerifiy != null)
        {
            EditorGUILayout.HelpBox(
                validMessage,
                MessageType.Info,
                true);
        }
        else
        {
            EditorGUILayout.HelpBox(
                notValidMessage,
                MessageType.Warning,
                true);
        }
    }
}
