using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour, IGameStartHandler, IGameRestartHandler, IGamePauseHandler, IGameDefeatHandler, IGameFinishHandler, ITimeRewindHandler
{
    private static AudioSource _audioSource;

    public static event Action<float> TimeChanged;
    public static event Action IsPlayingChanged;
    public static event Action IsPausedChanged;

    public static float Lenght => _audioSource.clip.length;
    public static float Time => _audioSource.time;
    public static bool IsPlaying { get; private set; }
    public static bool IsPaused { get; private set; }

    private bool DesiredGameState => GameState.State == GameState.States.Playing||
        GameState.State == GameState.States.Paused;

    public static void Play()
    {
        _audioSource.PlayScheduled(MusicSettings.StartOffset);
        IsPlaying = true;
        IsPaused = false;
        IsPlayingChanged?.Invoke();
    }
    public static void Stop()
    {
        _audioSource.Stop();
        IsPlaying = false;
        IsPaused = false;
        IsPlayingChanged?.Invoke();
    }
    public static void Pause()
    {
        _audioSource.Pause();
        IsPaused = true;
        IsPausedChanged?.Invoke();
    }
    public static void Resume()
    {
        _audioSource.UnPause();
        IsPaused = false;
        IsPausedChanged?.Invoke();
    }

    public static void RewindMusic(float rawTime)
    {
        float time = Mathf.Clamp(rawTime, 0, MusicSettings.MusicLength);
        TimeChanged?.Invoke(_audioSource.time = time);
    }

    public void OnGameStarted() => Play();
    public void OnGameRestarted() => Play();
    public void OnGamePaused() => Pause();
    public void OnGameResumed() => Resume();
    public void Defeat() => Stop();
    public void OnGameFinished() => Stop();
    public void ApplyPointInTime(object data) => _audioSource.time = (float)data;
    public object GetPointInTime() => _audioSource.time;

    private void Awake() => _audioSource = GetComponent<AudioSource>();

    private void FixedUpdate()
    {
        if (!DesiredGameState || !_audioSource.isPlaying) return;
        TimeChanged?.Invoke(Time);
    }

    private void OnEnable()
    {
        GameStarter.Register(this);
        GameRestarter.Register(this);
        GameDefeater.Register(this);
        GameFinisher.Register(this);
        GameSetPause.Register(this);
        TimeRewinder.Register(this);
    }
    private void OnDisable()
    {
        GameStarter.UnRegister(this);
        GameRestarter.UnRegister(this);
        GameDefeater.UnRegister(this);
        GameFinisher.UnRegister(this);
        GameSetPause.UnRegister(this);
        TimeRewinder.UnRegister(this);
    }
}
