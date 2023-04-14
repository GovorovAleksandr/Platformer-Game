using UnityEngine;
using UnityEngine.UI;

public class TimeRewindUI : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Image[] _progressFills;

    private void Rewinding(bool isRewinding) => _panel.SetActive(isRewinding);
    private void UpdateProgressFill(float progress)
    {
        foreach (Image progressFill in _progressFills)
            progressFill.fillAmount = progress;
    }

    private void OnEnable()
    {
        TimeRewinder.Rewinding += Rewinding;
        TimeRewinder.RewindingProgress += UpdateProgressFill;
    }
    private void OnDisable()
    {
        TimeRewinder.Rewinding -= Rewinding;
        TimeRewinder.RewindingProgress -= UpdateProgressFill;
    }
}
