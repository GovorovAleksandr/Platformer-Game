using UnityEngine;

public class BackgroundParticles : MonoBehaviour
{
    [SerializeField] private Transform _particles;
    [SerializeField] private Transform _followTarget;

    private void FixedUpdate() => _particles.position = _followTarget.position;
}
