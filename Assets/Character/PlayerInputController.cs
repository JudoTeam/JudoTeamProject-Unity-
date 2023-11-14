using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private float _movespeed;
    private PlayerInput _input;
    //private Vector2 _moveDirection;
     private Animator _animator;
     void Start()
    {
         _animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 movedirection = _input.Player.Move.ReadValue<Vector2>();

        Move(movedirection);
    }

    /*public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        Move(_moveDirection);
    }*/

    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = _movespeed * Time.deltaTime;
        
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirection * scaledMoveSpeed;
    }

    // Start is called before the first frame update
   
    
    
    
}
