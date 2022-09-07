using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SetAndAssignAssetBundleNames : EditorWindow
{
    public string editorWindowText = "Escolha um nome para o AssetBundle: ";
    string inputText;

    void OnGUI()
    {
        GUILayout.Label("Nome do novo AssetBundle", EditorStyles.boldLabel);
        inputText = EditorGUILayout.TextField(inputText);

        if (GUILayout.Button("Definir novo AssetBundle"))
        {
            SetAssetBundle(inputText);
            Close();
        }

        if (GUILayout.Button("Cancelar"))
            Close();
    }

    [MenuItem("AssetBundles/Set New AssetBundle Name", false, 0)]
    static void Init()
    {
        EditorWindow window = EditorWindow.CreateInstance<SetAndAssignAssetBundleNames>();
        window.ShowUtility();
    }

    static void SetAssetBundle(string name)
    {
        AssetDatabase.RemoveUnusedAssetBundleNames();
        if (name == "") name = "newbundle";

        string[] assets = Directory.GetFiles("Assets/Animations/");
        if (assets.Length > 0)
        {
            foreach (var assetPath in assets)
            {
                if (!assetPath.Contains(".meta"))
                {
                    AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                    assetImporter.assetBundleName = name;
                    Debug.Log("Loaded " + assetPath);
                }
            }
        }
        else
        {
            Debug.Log("No Assets in Folder 'Animations'");
        }
    }
}
