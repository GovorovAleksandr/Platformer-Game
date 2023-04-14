using UnityEngine;

public class PracticeModeHUD : MonoBehaviour, IGameModeDependent
{
    [SerializeField] private GameObject _practiceGameObject;

    public void OnEnabledNormalMode() => _practiceGameObject.SetActive(false);
    public void OnEnabledPracticeMode() => _practiceGameObject.SetActive(true);

    private void OnEnable() => PracticeMode.Register(this);
    private void OnDisable() => PracticeMode.UnRegister(this);
}
