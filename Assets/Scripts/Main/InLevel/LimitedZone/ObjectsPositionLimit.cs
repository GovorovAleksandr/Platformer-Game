using UnityEngine;

[RequireComponent(typeof(LimitedZone))]
public class ObjectsPositionLimit : MonoBehaviour
{
    [SerializeField] private Transform _targetsParent;

    private LimitedZone _limitedZone;

    private void Start()
    {
        _limitedZone = GetComponent<LimitedZone>();

        Vector2Bool limitMin = _limitedZone.LimitMin;
        Vector2Bool limitMax = _limitedZone.LimitMax;

        foreach (Transform target in _targetsParent)
        {
            LimitedObject limiter = target.gameObject.AddComponent<LimitedObject>();

            Vector3 min = _limitedZone.Min - target.transform.localPosition;
            Vector3 max = _limitedZone.Max - target.transform.localPosition - (Vector3)Vector2.one;

            limiter.LimitMin = limitMin;
            limiter.LimitMax = limitMax;
            limiter.Min = min;
            limiter.Max = max;
        }
    }
}