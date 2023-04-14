using UnityEngine;
using TMPro;

[DisallowMultipleComponent]
public class AirJumpsUI : MonoBehaviour
{
    [SerializeField] private AirJump _airJump;
    [SerializeField] private TextMeshProUGUI _text;

    public void ShowRemainingAirJumps(int value) => _text.text = value.ToString();
    private void OnEnable() => _airJump.JumpsLeftChanged += ShowRemainingAirJumps;
    private void OnDisable() => _airJump.JumpsLeftChanged -= ShowRemainingAirJumps;
}