using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private float speed = 8f;
    private float rotationspeed = 10f;
    private float slideforce = 5f;
    public Transform groundCheckerTransform;
    public LayerMask notPlayerMask;
    public TriggerAxeUp triggerAxeUp;
    private float jumpForce = 7f;
    [SerializeField]
    private Camera _playerCamera;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {   
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        Vector3 directionVector = new Vector3(horizontal, 0 ,vertical);
        
        // Vector3 forwardMovement = new Vector3(_playerCamera.transform.forward.x, 0, _playerCamera.transform.forward.z) * vertical;
        // Vector3 horizontalMovement = _playerCamera.transform.right * horizontal;
        // Vector3 directionVector = forwardMovement + horizontalMovement;
        if(directionVector.magnitude > Mathf.Abs(0.05f))
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationspeed);

        _animator.SetFloat("speed", Vector3.ClampMagnitude(directionVector, 1).magnitude); 

        Vector3 moveDir = Vector3.ClampMagnitude(directionVector, 1) * speed;   
        _rigidbody.velocity = new Vector3(moveDir.x, _rigidbody.velocity.y, moveDir.z);
        _rigidbody.angularVelocity = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGround())
            {
                Jump();
            }
            else
            {
                DoubleJump();
            }
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            Slide();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        if(IsGround())
        {
            _animator.SetBool("isinair", false);
        }
        else
        {
            _animator.SetBool("isinair", true);
        }
    }
    bool IsGround()
    {
        return Physics.CheckSphere(groundCheckerTransform.position, 0.3f, notPlayerMask);
    }
    void Jump()
    {
        if(IsGround())
        {
            _animator.SetTrigger("jumpTr");
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
    }
    void DoubleJump()
    {
        if(!IsGround())
        {
            _animator.SetTrigger("jumpTr");
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
    }
    void Slide()
    {        
        if(IsGround())
        {
            _animator.SetTrigger("slideTr");           
            _rigidbody.AddForce(Vector3.forward * slideforce, ForceMode.Impulse);      
            
        }
    }
    void Attack()
    {   
        if(triggerAxeUp.Axe != null && (triggerAxeUp.Axe.activeSelf || triggerAxeUp.Axe.activeInHierarchy))
        {
            _animator.SetTrigger("AttackTr_Weapons");
        }
        else
            _animator.SetTrigger("AttackTr");
    }

}
