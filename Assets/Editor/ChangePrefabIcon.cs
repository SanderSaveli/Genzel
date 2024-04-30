using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class CustomPrefabIcons
{
    static CustomPrefabIcons()
    {
        EditorApplication.projectWindowItemOnGUI += ChangePrefabIcon;
    }

    private static void ChangePrefabIcon(string guid, Rect selectionRect)
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));

        if (prefab != null)
        {
            PrefabIcon prefabIcon = prefab.GetComponent<PrefabIcon>();

            if (prefabIcon != null && prefabIcon.customIcon != null)
            {
                Rect offsetRect = new Rect(selectionRect);
                offsetRect.width = selectionRect.width * 0.98f;
                offsetRect.height = selectionRect.height * 0.83f;
                offsetRect.x += selectionRect.width - offsetRect.width;

                EditorGUI.DrawPreviewTexture(offsetRect, prefabIcon.customIcon);
            }
        }
    }
}
