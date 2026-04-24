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
    [Header("Grpund Check")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundCheckDistance;

    [Space]
    [Header("Interaction")]
    [SerializeField] LayerMask interactableMask;
    [SerializeField] float interactableDistance = 3.0f;

    Rigidbody _rb;
    Player_InputActions _input;
    CapsuleCollider _capsuleCollider;

    Vector2 _moveInput;
    Vector2 _lookInput;
    float _cameraPitch;
    bool _isGrounded;
    bool _isCursorVisible;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null) Debug.LogError($"Rigidbody Missing in {gameObject.name}");
        _rb.freezeRotation = true;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;

        _capsuleCollider = GetComponent<CapsuleCollider>();
        if (_capsuleCollider == null) Debug.LogError($"Missing Component {nameof(CapsuleCollider)} in {gameObject.name}");

        _input = new Player_InputActions(); //Input Action asset

        //Check for missing references
        if (cameraTransform == null) Debug.LogError($"Missing Reference {nameof(cameraTransform)} in {gameObject.name}");
    }
    private void OnEnable()
    {
        _input.Player.Enable();
        _input.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _input.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
        _input.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _input.Player.Look.canceled += ctx => _lookInput = Vector2.zero;
        _input.Player.Jump.performed += ctx => TryJump();
        _input.Player.Interact.performed += ctx => TryInteracting();
        _input.Player.Codex.performed += ctx => _isCursorVisible = true;
        _input.Player.Codex.canceled += ctx => _isCursorVisible = false;
        _input.Overlay.Codex.performed -= ctx => _isCursorVisible = true;
        _input.Overlay.Codex.canceled -= ctx => _isCursorVisible = false;
    }
    private void OnDisable() => _input.Player.Disable();

    private void Start()
    {
    }
    void Update()
    {
        HandleMouseVisibility();
        HandleLook();
        CheckGround();
        ApplyDrag();
    }
    private void FixedUpdate()
    {
        HandleMovement();
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

        _isGrounded = Physics.SphereCast(
            transform.position,
            0.4f,
            Vector3.down,
            out _,
            capsuleHalfHeight + groundCheckDistance,
            groundMask
        );
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

    void HandleMouseVisibility()
    {
        Cursor.visible = _isCursorVisible;
        Cursor.lockState = _isCursorVisible ? CursorLockMode.Confined : CursorLockMode.Locked;

        //if (_isCursorVisible)
        //{ 
        //    _input.Player.Disable();
        //    _input.Overlay.Enable();
        //}
        //else 
        //{ 
        //    _input.Player.Enable(); 
        //    _input.Overlay.Disable();
        //}

    }
    void TryInteracting()
    {
        Ray ray = new(cameraTransform.position, cameraTransform.forward * 10);
        if (Physics.Raycast(ray, out RaycastHit hit, interactableDistance, interactableMask))
            if (hit.collider.TryGetComponent<EntryUnlocker>(out EntryUnlocker unlocker))
                unlocker.UnlockEntry();
    }
}
