using UnityEngine;

[RequireComponent(typeof(LimitedZone))]
public class NoGravityZone : MonoBehaviour
{
    private LimitedZone _limitedZone;

    private void Awake() => _limitedZone = GetComponent<LimitedZone>();

    private void TriggerEnter(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerGravity playerGravitation))
            playerGravitation.Disable();
    }

    private void TriggerExit(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerGravity playerGravitation))
            playerGravitation.Enable();
    }

    private void OnEnable()
    {
        _limitedZone.TriggerEntered += TriggerEnter;
        _limitedZone.TriggerCameOut += TriggerExit;
    }
    private void OnDisable()
    {
        _limitedZone.TriggerEntered -= TriggerEnter;
        _limitedZone.TriggerCameOut -= TriggerExit;
    }
}