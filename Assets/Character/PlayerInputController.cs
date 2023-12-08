using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.InputSystem;
public class PlayerInputController : MonoBehaviour
{
    [SerializeField] 
    private float _movespeed;
    private PlayerInput _input;

     private Animator _animator;
     private Rigidbody _rigidbody;
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
     

    private void Move(Vector2 direction)
    {
        
        float scaledMoveSpeed = _movespeed * Time.deltaTime;
   
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        transform.position += moveDirection * scaledMoveSpeed;
        
    }

    // Start is called before the first frame update
   
    
    
    
}
