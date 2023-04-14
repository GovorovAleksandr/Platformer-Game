using UnityEngine;

public class BackgroundPulsing : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    [SerializeField] [ColorUsage(false)]
    private Color PulseColor;

    private bool DesiredGameState => GameState.State == GameState.States.Playing;

    private void FixedUpdate()
    {
        if (!DesiredGameState) return;
        Camera.backgroundColor = PulseColor * AudioLoudnessTester.ClipLoudness;
    }
}
