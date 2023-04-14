using Mirror;
using UnityEngine;

[RequireComponent(typeof(PlayerDefeat))]
public class DefeatParticles : NetworkBehaviour
{
    [SerializeField] private ParticleSystem _particles;

    private PlayerDefeat _playerDefeat;

    private void Play() => _particles.Play();

    private void Awake() => _playerDefeat = GetComponent<PlayerDefeat>();
    private void OnEnable() => _playerDefeat.Defeated += Play;
    private void OnDisable() => _playerDefeat.Defeated -= Play;
}