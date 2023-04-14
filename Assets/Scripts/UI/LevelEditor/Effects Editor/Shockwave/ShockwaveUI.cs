using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShockwaveUI : BaseEffectUI
{
    [SerializeField] private Slider _upperFeatherSlider;
    [SerializeField] private TMP_InputField _upperFeatherInputField;

    [SerializeField] private Slider _bottomFeatherSlider;
    [SerializeField] private TMP_InputField _bottomFeatherInputField;

    [SerializeField] private Slider _rippleIntensitySlider;
    [SerializeField] private TMP_InputField _rippleIntensityInputField;

    protected override void UpdateInfo()
    {
        ShockwaveParameters parameters = EffectParameters as ShockwaveParameters;


    }
}
