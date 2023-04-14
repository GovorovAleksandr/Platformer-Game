using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MusicPlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LeftTimeText;
    [SerializeField] private TextMeshProUGUI PassedTimeText;
    [SerializeField] private Slider _timeLineSlider;

    private void OnTimeChanged(float time)
    {
        _timeLineSlider.value = time;
        PassedTimeText.text = FloatRounder.Round(time).ToString();
        LeftTimeText.text = FloatRounder.Round(MusicPlayer.Lenght - time).ToString();
    }

    private void Start() => _timeLineSlider.maxValue = MusicPlayer.Lenght;
    private void OnEnable()
    {
        OnTimeChanged(MusicPlayer.Time);
        MusicPlayer.TimeChanged += OnTimeChanged;
    }
    private void OnDisable() => MusicPlayer.TimeChanged -= OnTimeChanged;
}
