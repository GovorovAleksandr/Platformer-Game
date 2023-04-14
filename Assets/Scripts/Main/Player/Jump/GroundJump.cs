using UnityEngine;

[DisallowMultipleComponent]
public class GroundJump : JumpHandler
{
    private bool _isJumping = false;
    private UserInput _input;

    public void TryJump()
    {
        if (!GroundCheck.OnSurface || !Allowed || !isLocalPlayer) return;
        
        Jump();
    }

    private void FixedUpdate()
    {
        if (!_isJumping) return;

        TryJump();
    }

    public void StartJumping() => _isJumping = true;
    public void StopJumping() => _isJumping = false;
    private void Awake() => _input = new();

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Jump.performed += context => StartJumping();
        _input.Player.Jump.canceled += context => StopJumping();
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Jump.performed -= context => StartJumping();
        _input.Player.Jump.canceled -= context => StopJumping();
    }
}