#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EditorSample : Editor
{
    protected void CenteredText(string text, bool bold)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (bold) GUILayout.Label(text, EditorStyles.boldLabel);
        else GUILayout.Label(text);

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    protected void ShowBoolAndFloat(ref bool field1, ref float field2, string name)
    {
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label(name);
        field1 = EditorGUILayout.Toggle(field1, GUILayout.MaxWidth(1000f));

        if (field1) field2 = EditorGUILayout.FloatField(field2, GUILayout.MaxWidth(50f));

        EditorGUILayout.EndHorizontal();
    }
}
#endif