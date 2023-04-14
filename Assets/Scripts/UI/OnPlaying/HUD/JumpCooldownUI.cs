using UnityEngine;
using UnityEngine.UI;

public class JumpCooldownUI : MonoBehaviour, ITimeRewindHandler
{
    [SerializeField] private Image _cooldownFill;

    public void ApplyPointInTime(object data) => _cooldownFill.fillAmount = (float)data;
    public object GetPointInTime() => _cooldownFill.fillAmount;

    private void UpdateFills(float value) => _cooldownFill.fillAmount = value;

    private void OnEnable()
    {
        JumpBlocker.ExpiredTimeChanged += UpdateFills;
        TimeRewinder.Register(this);
    }
    private void OnDisable()
    {
        JumpBlocker.ExpiredTimeChanged -= UpdateFills;
        TimeRewinder.Register(this);
    }
}
