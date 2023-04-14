using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class LimitedObject : MonoBehaviour
{
    public Vector2Bool LimitMin;
    public Vector2Bool LimitMax;
    public Vector2 Min;
    public Vector2 Max;

    private ObjectProperties _startProperties;

    private bool DesiredGameState => GameState.State == GameState.States.Playing;

    private void Awake() => _startProperties = new(transform.position);

    private void FixedUpdate()
    {
        if (!DesiredGameState) return;
        LimitPosition();
    }

    private void LimitPosition()
    {
        Vector3 position = transform.position;
        Vector2 startPos = _startProperties.Position;
        Vector2 min = Min + startPos;
        Vector2 max = Max + startPos;

        if (LimitMin.x && position.x < min.x) position.x = min.x;
        if (LimitMax.x && position.x > max.x) position.x = max.x;
        if (LimitMin.y && position.y < min.y) position.y = min.y;
        if (LimitMax.y && position.y > max.y) position.y = max.y;

        transform.position = position;
    }

    #region Editor
    #if UNITY_EDITOR
    [CustomEditor(typeof(LimitedObject))]
    public class LimitObjectPositionEditor : EditorSample
    {
        public override void OnInspectorGUI()
        {
            LimitedObject limitObjectPosition = target as LimitedObject;
            serializedObject.Update();

            CenteredText("Required", true);

            EditorGUILayout.Space(5f);

            EditorGUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            ShowBoolAndFloat(ref limitObjectPosition.LimitMin.x, ref limitObjectPosition.Min.x, "Min X");
            ShowBoolAndFloat(ref limitObjectPosition.LimitMax.x, ref limitObjectPosition.Max.x, "Max X");

            ShowBoolAndFloat(ref limitObjectPosition.LimitMin.y, ref limitObjectPosition.Min.y, "Min Y");
            ShowBoolAndFloat(ref limitObjectPosition.LimitMax.y, ref limitObjectPosition.Max.y, "Max Y");

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
            if (GUI.changed && !EditorApplication.isPlaying)
            {
                Undo.RecordObject(target, "Limit Object Position");
                EditorUtility.SetDirty(target);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
    }
    #endif
    #endregion
}
