using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

[DisallowMultipleComponent]
public class FollowTarget : MonoBehaviour, ITimeRewindHandler, IGameRestartHandler
{
    [SerializeField] private bool _targetIsPlayer;
    [SerializeField] private Transform _target;

    [SerializeField] private Vector2Bool _follow;
    [SerializeField] private Vector2Bool _useOffset;

    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _smoothness;

    [SerializeField] private bool _lookAtTarget;
    [SerializeField] private float _rotationSmoothness;

    [SerializeField] private ObjectProperties _startProperties;

    private bool DesiredGameState => GameState.State == GameState.States.Playing;

    public object GetPointInTime() => new ObjectProperties(transform.position, transform.rotation);
    public void ApplyPointInTime(object data)
    {
        ObjectProperties positionAndRotation = (ObjectProperties)data;
        transform.SetPositionAndRotation(positionAndRotation.Position, positionAndRotation.Rotation);
    }

    public void OnGameRestarted()
    {
        transform.SetPositionAndRotation(_startProperties.Position, _startProperties.Rotation);
    }
    private void Awake() => _startProperties = new(transform.position, transform.rotation);

    private void FixedUpdate()
    {
        if (TimeRewinder.IsRewinding || !DesiredGameState || LocalPlayerReference.Transform == null) return;

        if (_lookAtTarget)
        {
            Vector3 target = _targetIsPlayer ? LocalPlayerReference.Transform.position : _target.transform.position;
            transform.rotation = LookAt(target);
        }
        transform.position = FollowingPosition();
    }

    private Vector3 FollowingPosition()
    {
        Vector3 position = transform.position;
        Vector2 target = _targetIsPlayer ? LocalPlayerReference.Transform.position : _target.transform.position;

        if (_follow.x)
        {
            if (_useOffset.x) target.x += _offset.x;
            position.x = Mathf.Lerp(position.x, target.x, _smoothness.x);
        }

        if (_follow.y)
        {
            if (_useOffset.y) target.y += _offset.y;
            position.y = Mathf.Lerp(position.y, target.y, _smoothness.y);
        }

        return position;
    }

    private Quaternion LookAt(Vector3 target)
    {
        Vector2 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = transform.rotation;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        return Quaternion.Lerp(rotation, targetRotation, _rotationSmoothness);
    }

    private void OnEnable()
    {
        TimeRewinder.Register(this);
        GameRestarter.Register(this);
    }

    private void OnDisable()
    {
        TimeRewinder.UnRegister(this);
        GameRestarter.UnRegister(this);
    }

    #region Editor
    #if UNITY_EDITOR
    [CustomEditor(typeof(FollowTarget))]
    public class FollowTargetEditor : EditorSample
    {
        public override void OnInspectorGUI()
        {
            FollowTarget followTarget = target as FollowTarget;

            serializedObject.Update();

            CenteredText("Required", true);

            EditorGUILayout.Space(5f);

            followTarget._targetIsPlayer = GUILayout.Toggle(followTarget._targetIsPlayer, "Target Is Player");
            if (!followTarget._targetIsPlayer)
            {
                EditorGUILayout.Space(5f);
                followTarget._target = EditorGUILayout.ObjectField("Target", followTarget._target, typeof(Transform), true) as Transform;
            }

            EditorGUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();
            followTarget._follow.x = GUILayout.Toggle(followTarget._follow.x, "Follow X");
            followTarget._follow.y = GUILayout.Toggle(followTarget._follow.y, "Follow Y");
            followTarget._lookAtTarget = GUILayout.Toggle(followTarget._lookAtTarget, "Look At Target");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20f);

            if (followTarget._follow.x || followTarget._follow.y || followTarget._lookAtTarget)
            {
                CenteredText("Optional", true);
                EditorGUILayout.Space(5f);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            if (followTarget._follow.x)
            {
                ShowBoolAndFloat(ref followTarget._useOffset.x, ref followTarget._offset.x, "Offset X");
                GUILayout.Label("Smoothness X");
                followTarget._smoothness.x = EditorGUILayout.Slider(followTarget._smoothness.x, 0.01f, 1f);
            }

            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();

            if (followTarget._follow.y)
            {
                ShowBoolAndFloat(ref followTarget._useOffset.y, ref followTarget._offset.y, "Offset Y");
                GUILayout.Label("Smoothness Y");
                followTarget._smoothness.y = EditorGUILayout.Slider(followTarget._smoothness.y, 0.01f, 1f);
            }

            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();

            if (followTarget._lookAtTarget)
            {
                GUILayout.Label("Smoothness Rotation");
                followTarget._rotationSmoothness = EditorGUILayout.Slider(followTarget._rotationSmoothness, 0.01f, 1f);
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();

            if (GUI.changed && !EditorApplication.isPlaying)
            {
                Undo.RecordObject(target, "Look at Target");
                EditorUtility.SetDirty(target);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
        }
    }
    #endif
    #endregion
}