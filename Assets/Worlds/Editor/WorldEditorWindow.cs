using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldEditorWindow : EditorWindow
{
    [MenuItem("Window/General/World")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        WorldEditorWindow window = (WorldEditorWindow)EditorWindow.GetWindow(typeof(WorldEditorWindow));
        window.Show();
        window.minSize = new Vector2(EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight);
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Light"))
        {
            WorldController.lightWorldShowing = true;
            Selection.activeObject = WorldController.LightTilemap;
        }

        if (GUILayout.Button("Dark"))
        {
            WorldController.lightWorldShowing = false;
            Selection.activeObject = WorldController.DarkTilemap;
        }

        EditorGUILayout.EndHorizontal();
    }
}
