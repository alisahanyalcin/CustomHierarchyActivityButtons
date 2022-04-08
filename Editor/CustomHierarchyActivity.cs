using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class CustomHierarchyActivity
{
    private static readonly float IconWidth = 15;
    private static readonly Vector2 IconPadding = new Vector2(3, 0);
    private static readonly GUIContent ActiveIcon = EditorGUIUtility.IconContent("d_scenevis_visible_hover@2x");
    private static readonly GUIContent InActiveIcon = EditorGUIUtility.IconContent("d_scenevis_hidden_hover@2x");

    static CustomHierarchyActivity()
    {
        EditorApplication.hierarchyWindowItemOnGUI += DrawIconOnHierarchyPanel;
    }

    private static void DrawIconOnHierarchyPanel(int instanceID, Rect rect)
    {
        GUIStyle icon = new GUIStyle();
        GameObject instanceGameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (instanceGameObject == null) return;

        EditorGUIUtility.SetIconSize(new Vector2(IconWidth, IconWidth));
        var iconDrawRect = new Rect(rect.xMax - IconPadding.x, rect.yMin, rect.width, rect.height);

        var iconGUIContent = new GUIContent(ActiveIcon);
        switch (instanceGameObject.activeInHierarchy)
        {
            case true:
                iconGUIContent = new GUIContent(ActiveIcon);
                break;
            case false:
                iconGUIContent = new GUIContent(InActiveIcon);
                break;
        }
        
        GUI.Label(iconDrawRect, iconGUIContent, icon);

        bool toggle = EditorGUI.Toggle(iconDrawRect, instanceGameObject.activeInHierarchy, icon);
        if (toggle != instanceGameObject.activeInHierarchy)
            instanceGameObject.SetActive(toggle);
        
        EditorGUIUtility.SetIconSize(Vector2.zero);
    }
}