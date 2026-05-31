using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    [Space]
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float groundDrag = 8f;
    [SerializeField] float airDrag = 1f;

    [Space]
    [Header("Look")]
    [SerializeField] float mouseSensitivity = 0.1f;
    [SerializeField] float verticalClamp = 85f;
    [SerializeField] Transform cameraTransform;

    [Space]
    [Header("Grpound Check")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckDistance;

    [Space]
    [Header("Footsteps")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip outdoorFootstep;
    [SerializeField] private AudioClip indoorFootstep;
    [SerializeField] private LayerMask indoorMask;
    [SerializeField] private float stepInterval = 0.45f;

    private float stepTimer;
    RaycastHit _groundHit;

    Rigidbody _rb;
    CapsuleCollider _capsuleCollider;

    Vector2 _moveInput;
    Vector2 _lookInput;
    float _cameraPitch;
    bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null) Debug.LogError($"Rigidbody Missing in {gameObject.name}");
        _rb.freezeRotation = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;

        _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider == null) Debug.LogError($"Missing Component {nameof(CapsuleCollider)} in {gameObject.name}");

        //Check for missing references
        if (cameraTransform == null) Debug.LogError($"Missing Reference {nameof(cameraTransform)} in {gameObject.name}");
    }
    private void OnEnable()
    {
        InputManager.Instance.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        InputManager.Instance.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        InputManager.Instance.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        InputManager.Instance.Player.Look.canceled += ctx => _lookInput = Vector2.zero;
        InputManager.Instance.Player.Jump.performed += ctx => TryJump();
        InputManager.Instance.Player.Codex.performed += ctx => EventsManager.Broadcast(new OnCodexOpened());
        InputManager.Instance.UI.CloseMenu.performed += ctx => EventsManager.Broadcast(new OnCodexClosed());

        InputManager.Instance.Global.Pause.canceled += ctx => GameManager.Instance.TogglePause();
    }
    private void OnDisable()
    {
        InputManager.Instance.Player.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        InputManager.Instance.Player.Move.canceled -= ctx => _moveInput = Vector2.zero;
        InputManager.Instance.Player.Look.performed -= ctx => _lookInput = ctx.ReadValue<Vector2>();
        InputManager.Instance.Player.Look.canceled -= ctx => _lookInput = Vector2.zero;
        InputManager.Instance.Player.Jump.performed -= ctx => TryJump();
        InputManager.Instance.Player.Codex.performed -= ctx => EventsManager.Broadcast(new OnCodexOpened());
        InputManager.Instance.UI.CloseMenu.performed -= ctx => EventsManager.Broadcast(new OnCodexClosed());


        InputManager.Instance.Global.Pause.canceled -= ctx => GameManager.Instance.TogglePause();
    }
    void Update()
    {
        HandleLook();
        CheckGround();
        ApplyDrag();
    }

    private void HandleFootsteps()
    {
        bool isMoving = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z).magnitude > 0.1f;

        if (_isGrounded && isMoving)
        {
            bool isIndoor = (indoorMask.value & (1 << _groundHit.collider.gameObject.layer)) != 0;
            AudioClip clip = isIndoor ? indoorFootstep : outdoorFootstep;

            if (footstepSource.clip != clip)
            {
                footstepSource.Stop();
                footstepSource.clip = clip;
                footstepSource.Play();
                stepTimer = stepInterval;
            }

            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                footstepSource.Play();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval;
            footstepSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleFootsteps();
    }
#region LOOKING AND MOVEMENT
    void HandleLook()
    {
        transform.Rotate(Vector3.up, _lookInput.x * mouseSensitivity, Space.World);

        _cameraPitch -= _lookInput.y * mouseSensitivity;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -verticalClamp, verticalClamp);
        cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
    }
    void HandleMovement()
    {
        Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        _rb.AddForce(move * moveSpeed, ForceMode.VelocityChange);

        // Clamp horizontal speed so the player doesn't accelerate forever
        Vector3 flat = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        if (flat.magnitude > moveSpeed)
        {
            Vector3 clamped = flat.normalized * moveSpeed;
            _rb.linearVelocity = new Vector3(clamped.x, _rb.linearVelocity.y, clamped.z);
        }
    }
    void CheckGround()
    {
        float capsuleHalfHeight = _capsuleCollider.height / 2f;

        _isGrounded = Physics.SphereCast( transform.position, 0.4f, Vector3.down, out _groundHit, capsuleHalfHeight + groundCheckDistance, groundMask);
    }
    void ApplyDrag()
    {
        _rb.linearDamping = _isGrounded ? groundDrag : airDrag;
    }
    void TryJump()
    {
        if (!_isGrounded) return;
        // Reset Y velocity before jumping for consistent jump height
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    #endregion
}
