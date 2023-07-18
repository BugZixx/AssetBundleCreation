using System;
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
        string databaseExcel = "";
        AssetDatabase.RemoveUnusedAssetBundleNames();

        string[] assets = Directory.GetFiles("Assets/Animations/");
        if (assets.Length > 0)
        {
            int i = 0, j = 1;
            foreach (var assetPath in assets)
            {
                if (!assetPath.Contains(".meta"))
                {
                    AssetImporter assetImporter = AssetImporter.GetAtPath(assetPath);
                   

                    string assetName = assetImporter.assetPath;
                    assetName = assetName.Replace("Assets/Animations/mark@", "");
                    assetName = assetName.Replace(".fbx", "");
                    assetImporter.assetBundleName = name + "bundle" + assetName;
                    string assetBundleName = assetImporter.assetBundleName;

                   

                    //databaseExcel = databaseExcel + assetName + "\t" + assetBundleName + "\t" + assetName + "\t" + "0" + "\t" + "0" + "\n";
                    databaseExcel = databaseExcel + "\ndocker cp ./" + assetBundleName + " 42b43df05fee:/ivling/spaCy_POSTagger/static/AssetBundles/";

                    i++;
                    if(i >= 1)
                    {
                        i = 0;
                        j++;
                    }
                }
            }
            GUIUtility.systemCopyBuffer = databaseExcel;
            WriteString(databaseExcel);
        }
        else
        {
            Debug.Log("No Assets in Folder 'Animations'");
        }
    }



    static void WriteString(string s)
    {
        string path = "Assets/StreamingAssets/bundlesToBD.txt";
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(s);
        writer.Close();
        //Re-import the file to update the reference in the editor
       // AssetDatabase.ImportAsset(path); 
       // TextAsset asset = Resources.Load("test");
        //Print the text from the file
      //  Debug.Log(asset.text);
    }
}
