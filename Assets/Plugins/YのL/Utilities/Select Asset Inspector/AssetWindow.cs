using UnityEditor;
using UnityEngine;

public static class ShowAssetWindow
{
    public static void ShowWindow(this Object asset) => AssetWindow.Show(asset);
}

public class AssetWindow : EditorWindow
{
    private Object asset;
    private Editor assetEditor;

    public static AssetWindow Show(Object asset)
    {
        var window = CreateWindow<AssetWindow>($"{asset.name} | {asset.GetType().Name}");
        window.asset = asset;
        window.assetEditor = Editor.CreateEditor(asset);
        return window;
    }

    private void OnGUI()
    {
        GUI.enabled = false;
        asset = EditorGUILayout.ObjectField("Asset", asset, asset.GetType(), false);
        GUI.enabled = true;

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        assetEditor.OnInspectorGUI();
        EditorGUILayout.EndVertical();
    }
}
   