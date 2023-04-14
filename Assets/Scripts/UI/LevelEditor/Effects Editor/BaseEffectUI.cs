using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseEffectUI : MonoBehaviour
{
    [SerializeField] protected EffectParameters EffectParameters;
    [SerializeField] private Slider _startOffsetSlider;
    [SerializeField] private TMP_InputField _startOffsetInputField;
    [SerializeField] private Slider _durationSlider;
    [SerializeField] private TMP_InputField _durationInputField;
    protected abstract void UpdateInfo();
    private void OnEnable() => EffectParameters.ValueChanged += UpdateInfo;
    private void OnDisable() => EffectParameters.ValueChanged -= UpdateInfo;
}