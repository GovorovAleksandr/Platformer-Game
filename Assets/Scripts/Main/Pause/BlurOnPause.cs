using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlurOnPause : MonoBehaviour, IGamePauseHandler, IGameRestartHandler
{
    [SerializeField] private PostProcessVolume volume;
    private DepthOfField blur;

    private void Awake() => volume.profile.TryGetSettings<DepthOfField>(out blur);

    public void OnGamePaused() => blur.active = true;
    public void OnGameResumed() => blur.active = false;
    public void OnGameRestarted() => blur.active = false;

    private void OnEnable()
    {
        GameSetPause.Register(this);
        GameRestarter.Register(this);
    }
    private void OnDisable()
    {
        GameSetPause.UnRegister(this);
        GameRestarter.UnRegister(this);
    }
}
