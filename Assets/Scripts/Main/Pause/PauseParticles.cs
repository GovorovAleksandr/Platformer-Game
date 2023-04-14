using UnityEngine;

public class PauseParticles : MonoBehaviour, IGamePauseHandler, IGameRestartHandler
{
    [SerializeField] private ParticleSystem _particles;

    public void OnGamePaused() => _particles.Pause();
    public void OnGameResumed() => _particles.Play();
    public void OnGameRestarted() => _particles.Play();

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
