using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class ABBA : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _input;
    private Rigidbody _rb;
    private InputAction move;
    [SerializeField]
    private float _movementForce = 1f;
    [SerializeField]
    private float _jumpForce = 5f;
    [SerializeField]
    private float _maxSpeed = 5f;
    private Vector3 _forceDirection = Vector3.zero;

    [SerializeField]
    private Camera _playerCamera;
    

    // Start is called before the first frame update
    void Awake()
    {
        _input = new PlayerInput();
        _animator = this.GetComponent<Animator>();
        _rb = this.GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        _input.Player.Jump.started += DoJump;
        _input.Player.Attack.started += DoAttack;
        move = _input.Player.Move;
        _input.Player.Enable();
    }

    void OnDisable()
    {
        _input.Player.Jump.started -= DoJump;
        _input.Player.Attack.started -= DoAttack;
        _input.Player.Disable();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        _forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(_playerCamera) * _movementForce;
        _forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(_playerCamera) * _movementForce;

        _rb.AddForce(_forceDirection, ForceMode.Impulse);
        _forceDirection = Vector3.zero;

        if (_rb.velocity.y < 0f)
            _rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = _rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > _maxSpeed * _maxSpeed)
            _rb.velocity = horizontalVelocity.normalized * _maxSpeed + Vector3.up * _rb.velocity.y;

        LookAt();
    }

    private void LookAt()
    {
        Vector3 direction = _rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this._rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            _rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    
    private void DoJump(InputAction.CallbackContext obj)
    {
        if(IsGrounded())
        {
            _forceDirection += Vector3.up * _jumpForce;
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false; 
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        _animator.SetTrigger("attack");
    }
    
}
