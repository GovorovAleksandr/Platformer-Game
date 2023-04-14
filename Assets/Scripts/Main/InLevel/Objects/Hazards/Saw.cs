using UnityEngine;

public class Saw : Hazard, ITimeRewindHandler
{
    [SerializeField] private float _rotateSpeed;

    private GameState.States State => GameState.State;
    private bool DesiredGameState => State == GameState.States.Playing || State == GameState.States.Defeated;

    public void ApplyPointInTime(object data) => transform.rotation = (Quaternion)data;
    public object GetPointInTime() => transform.rotation;

    private void Update()
    {
        if (!DesiredGameState) return;
        transform.Rotate(new Vector3(0, 0, _rotateSpeed) * Time.deltaTime);
    }

    private void OnEnable() => TimeRewinder.Register(this);
    private void OnDisable() => TimeRewinder.UnRegister(this);
}
