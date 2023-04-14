using UnityEngine;

public class AudioLoudnessTester : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private float _currentUpdateTime = 0f;
    private float[] _clipSampleData;

    public static float ClipLoudness { get; private set; }

    [SerializeField] private float _updateStep = 0.1f;
    [SerializeField] private int _sampleDataLength = 1024;
    [SerializeField] private float _sizeFactor;
    [SerializeField] private float _minSize = 0;
    [SerializeField] private float _maxSize = 500;

    private void Awake() => _clipSampleData = new float[_sampleDataLength];

    private void Update()
    {
        _currentUpdateTime += Time.deltaTime;

        if (_audioSource != null && _currentUpdateTime >= _updateStep)
        {
            _currentUpdateTime = 0f;
            _audioSource.clip.GetData(_clipSampleData, _audioSource.timeSamples);
            ClipLoudness = 0f;

            foreach (var sample in _clipSampleData)
                ClipLoudness += Mathf.Abs(sample);

            ClipLoudness /= _sampleDataLength;
            ClipLoudness *= _sizeFactor;
            ClipLoudness = Mathf.Clamp(ClipLoudness, _minSize, _maxSize);
        }
    }
}
