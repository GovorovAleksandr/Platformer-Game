using UnityEngine;

public class PracticeModeButtons : MonoBehaviour
{
    [SerializeField] private GameObject _practiceModeButton;
    [SerializeField] private GameObject _normalModeButton;

    private void UpdateButtons(bool practiceEnabled)
    {
        _practiceModeButton.SetActive(!practiceEnabled);
        _normalModeButton.SetActive(practiceEnabled);
    }

    private void OnEnable() => UpdateButtons(PracticeMode.Enabled);
}
