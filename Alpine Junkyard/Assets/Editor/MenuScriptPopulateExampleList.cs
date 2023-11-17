using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

#nullable enable

public class MenuScriptPopulateExampleList
{
    [MenuItem("SSS/PopulateExampleList %&e")]
    public static void PopulateExampleList()
    {
        Debug.Log("Repopulating Texture List");
        var textureListExample = GameObject.FindFirstObjectByType<TextureListExample>();

        if (textureListExample != null)
        {
            var textures = new List<Texture2D>();

            var assetPath = Path.Combine("Assets", "Materials");
            var assetFolders = AssetDatabase.GetSubFolders(assetPath);
            var textureGuids = AssetDatabase.FindAssets("t:Texture2D", assetFolders);
            foreach (var guid in textureGuids)
            {
                var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid));
                textures.Add(texture);
            }

            textureListExample.SetMaterials(textures);
            EditorUtility.SetDirty(textureListExample);
        }
        else
        {
            Debug.LogError("No TextureListExample in this Scene");
        }
    }
}
