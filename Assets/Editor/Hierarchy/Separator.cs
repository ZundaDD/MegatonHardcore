using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[InitializeOnLoad] // 确保脚本在编辑器启动时运行
public class SeparatorDrawer : Editor
{
    static Texture2D backgroundT;
    static int frameoff = 1;

    static SeparatorDrawer()
    {
        backgroundT = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath("ba9c0c8a08f02314496276a80cb90fc7"));
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
    }

    static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID);
        
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject == null) return;
        if (!gameObject.TryGetComponent(out SeparatorComponent com)) return;
        
        //绘制区域
        Rect fullRect = selectionRect;
        fullRect.xMin = 31f;
        fullRect.xMax = EditorGUIUtility.currentViewWidth - frameoff;
        fullRect.yMin -= 1;
        fullRect.yMax += 1;

        Rect fillRect = selectionRect;
        fillRect.xMin = 31f + frameoff;
        fillRect.xMax = EditorGUIUtility.currentViewWidth - 2 * frameoff;
        fillRect.yMin -= 1 - frameoff;
        fillRect.yMax += 1 - frameoff;

        Color formColor = GUI.color;

        //准备样式
        GUIStyle boxstyle = new(GUIStyle.none);
        boxstyle.normal.background = backgroundT;
        boxstyle.normal.textColor = Color.white;
        boxstyle.fontStyle = FontStyle.Bold;
        boxstyle.alignment = TextAnchor.MiddleCenter;
        

        //绘制边框
        GUI.backgroundColor = Color.gray;
        GUI.Box(fullRect, "", boxstyle);
        GUI.backgroundColor = formColor;
        
        //绘制填充
        GUI.backgroundColor = com.FillColor;
        GUI.Box(fillRect, com.Text, boxstyle);
        GUI.backgroundColor = formColor;
        
    }

}