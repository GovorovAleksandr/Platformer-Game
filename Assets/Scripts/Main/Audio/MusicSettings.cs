using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(AudioSource))]
public class MusicSettings : MonoBehaviour
{
    public static event Action<float> VolumeChanged;

    [SerializeField] private float _startOffset;
    public static float StartOffset { get; private set; }

    [SerializeField] private bool _loop;
    public static bool Loop
    {
        get => _audio.loop;
        private set => _audio.loop = value;
    }

    public static float Volume
    {
        get => _audio.volume;
        private set => VolumeChanged?.Invoke(_audio.volume = value);
    }
    public static float MusicLength => _audio.clip.length;

    private static AudioSource _audio;

    public void ChangeStartOffset(float time) => StartOffset = time;
    public void ChangeStartOffset(string time) => StartOffset = float.Parse(time);
    public void ChangeVolume(float volume) => Volume = volume;
    public void ChangeLoop(bool loop) => Loop = loop;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        ChangeStartOffset(_startOffset);
        ChangeLoop(_loop);
    }

}
