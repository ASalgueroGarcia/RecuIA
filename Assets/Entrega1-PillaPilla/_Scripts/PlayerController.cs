using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")] 
    [SerializeField] private float speed;

    [Header("Camera Reference")]
    [SerializeField] private Transform cameraTransform;

    private Vector3 _moveDir = Vector3.zero;
    private PlayerInputActions _inputActions;
    
    private InputAction _moveAction;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _inputActions = new PlayerInputActions();
        _moveAction = _inputActions.MOVE.Movement;

        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    private void OnEnable()
    {
        _moveAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
    }

    private void OnDestroy()
    {
        _inputActions?.Dispose();
    }

    private void FixedUpdate()
    {
        Move();
        _rb.linearVelocity = new Vector3(_moveDir.x, _rb.linearVelocity.y, _moveDir.z);
    }

    private void Move()
    {
        var input = _moveAction.ReadValue<Vector2>();

        if (input != Vector2.zero)
        {
            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;
            camRight.y = 0f;
            camRight.Normalize();

            Vector3 worldDir = (camForward * input.y + camRight * input.x).normalized;

            _moveDir = worldDir * speed;
            transform.forward = worldDir;
        }
        else _moveDir = Vector3.zero;
    }
}