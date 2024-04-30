using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class PrefabIconsView
{
    static PrefabIconsView()
    {
        EditorApplication.projectWindowItemOnGUI += ChangePrefabIcon;
    }

    private static void ChangePrefabIcon(string guid, Rect selectionRect)
    {
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

        if (prefab != null)
        {
            PrefabIcon prefabIcon = prefab.GetComponent<PrefabIcon>();
            if (prefabIcon != null && prefabIcon.iconData.customIcon != null)
            {
                Rect offsetRect = new Rect(selectionRect);
                offsetRect.width = selectionRect.width * 0.98f;
                offsetRect.height = selectionRect.height * 0.83f;
                offsetRect.x += selectionRect.width - offsetRect.width;

                EditorGUI.DrawPreviewTexture(offsetRect, prefabIcon.iconData.customIcon);
            }
        }
    }
}
