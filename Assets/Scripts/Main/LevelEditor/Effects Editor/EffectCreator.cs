using UnityEngine;

public class EffectCreator : MonoBehaviour
{
    [SerializeField] private GameObject _effectsParent;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Shader _shader;

    public void Create(EffectParameters _parameters)
    {
        ShockWaveEffect effect = _effectsParent.AddComponent<ShockWaveEffect>();
    }
}