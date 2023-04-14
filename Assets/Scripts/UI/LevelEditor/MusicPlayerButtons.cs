using UnityEngine;

public class MusicPlayerButtons : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _stopButton;
    [SerializeField] private GameObject _resumeButton;
    [SerializeField] private GameObject _pauseButton;

    private void UpdateButtons()
    {
        _playButton.SetActive(!MusicPlayer.IsPlaying);
        _stopButton.SetActive(MusicPlayer.IsPlaying);
        _pauseButton.SetActive(!MusicPlayer.IsPaused && MusicPlayer.IsPlaying);
        _resumeButton.SetActive(MusicPlayer.IsPaused && MusicPlayer.IsPlaying);
    }

    private void OnEnable()
    {
        UpdateButtons();
        MusicPlayer.IsPlayingChanged += UpdateButtons;
        MusicPlayer.IsPausedChanged += UpdateButtons;
    }

    private void OnDisable()
    {
        MusicPlayer.IsPlayingChanged -= UpdateButtons;
        MusicPlayer.IsPausedChanged -= UpdateButtons;
    }

}