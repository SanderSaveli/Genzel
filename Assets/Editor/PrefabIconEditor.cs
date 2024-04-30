using UnityEngine;
using UnityEditor;

public class PrefabIconEditor : EditorWindow
{
    private Texture2D customIcon;

    [MenuItem("Assets/Change Icon")]
    private static void ChangeIcon()
    {
        PrefabIconEditor window = GetWindow<PrefabIconEditor>("Change Icon");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Custom Icon:");
        customIcon = (Texture2D)EditorGUILayout.ObjectField(customIcon, typeof(Texture2D), false);

        if (GUILayout.Button("Apply Icon"))
        {
            if (customIcon != null)
            {
                foreach (var obj in Selection.objects)
                {
                    GameObject prefab = obj as GameObject;
                    if (prefab != null)
                    {
                        ApplyCustomIconToPrefab(prefab, customIcon);
                    }
                }
            }
        }
    }

    private void ApplyCustomIconToPrefab(GameObject prefab, Texture2D icon)
    {
        PrefabIcon prefabIcon = prefab.GetComponent<PrefabIcon>();
        if (prefabIcon == null)
        {
            prefabIcon = prefab.AddComponent<PrefabIcon>();
        }
        prefabIcon.iconData.customIcon = icon;
    }
}
