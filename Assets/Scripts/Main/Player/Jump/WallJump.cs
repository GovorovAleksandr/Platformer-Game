using UnityEngine;

[DisallowMultipleComponent]
public class WallJump : JumpHandler
{
    [SerializeField] private SurfaceCheck _wallCheck;

    private UserInput _input;

    public void TryJump()
    {
        if (!_wallCheck.OnSurface || !Allowed || !isLocalPlayer) return;

        Jump();
    }

    private void Awake() => _input = new();

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += context => TryJump();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Jump.performed -= context => TryJump();
    }
}
