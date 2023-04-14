using UnityEngine;
using System;

public class EffectParameters : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _startOffset;
    [SerializeField] private float _duration;
    
    public event Action ValueChanged;

    public void ChangeStartOffset(float value) => _startOffset = value;
    public void ChangeStartOffset(string value) => _startOffset = float.Parse(value);
    public void ChangeStartOffset() => _startOffset = _audioSource.time;

    public void ChangeDurationTime(float value) => _duration = value;
    public void ChangeDurationTime(string value) => _duration = float.Parse(value);
}