using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider _volumeSlider;

    private void UpdateVolumeSlider(float volume) => _volumeSlider.value = volume;
    private void OnEnable()
    {
        MusicSettings.VolumeChanged += UpdateVolumeSlider;

        if(_volumeSlider == null) _volumeSlider = GetComponent<Slider>();

        _volumeSlider.value = MusicSettings.Volume;
    }
    private void OnDisable() => MusicSettings.VolumeChanged -= UpdateVolumeSlider;
}
