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
    public TriggerSwordUp triggerSwordUp;
    public TriggerRifleUp triggerRifleUp;
    private bool hasDoubleJumped = false;
    private float jumpForce = 6f;
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

    //     if(triggerSwordUp.Rifle_Player != null && (triggerSwordUp.Rifle_Player.activeSelf || triggerSwordUp.Rifle_Player.activeInHierarchy))
    //     {   
    //         _animator.SetBool("Rifle_Bool", true);
    //         _animator.SetFloat("speedRF", Vector3.ClampMagnitude(directionVector, 1).magnitude); 
    //     }
    //    else 
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
           //_animator.ResetTrigger("AttackTr");
        }

        if(IsGround())
        {
            _animator.SetBool("isinair", false);
        }
        else
        {
            _animator.SetBool("isinair", true);
        }
        
        if(Input.GetKeyDown(KeyCode.Mouse1) && triggerSwordUp.Sword_Player != null && (triggerSwordUp.Sword_Player.activeSelf || triggerSwordUp.Sword_Player.activeInHierarchy))
        {
            Block();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1) && triggerSwordUp.Sword_Player != null && (triggerSwordUp.Sword_Player.activeSelf || triggerSwordUp.Sword_Player.activeInHierarchy))
        {
            StopBlock();
        }

        // if(Input.GetKey(KeyCode.Mouse1) && (triggerRifleUp.Rifle_Player != null && (triggerRifleUp.Rifle_Player.activeSelf || triggerRifleUp.Rifle_Player.activeInHierarchy)))
        // {
        //     AIM();
        // }
        // else
        // {
        //     StopAIM();
        // }
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
            hasDoubleJumped = true;
        }
    }
    void DoubleJump()
    {
        if(hasDoubleJumped & !IsGround())
        {
            _animator.SetTrigger("jumpTr");
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasDoubleJumped = false;
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
        if(triggerSwordUp.Sword_Player != null && (triggerSwordUp.Sword_Player.activeSelf || triggerSwordUp.Sword_Player.activeInHierarchy))
        {
            _animator.SetTrigger("AttackTr_Sword");
            //_animator.ResetTrigger("AttackTr_Sword");
        }
        else if(triggerAxeUp.Axe_Player != null && (triggerAxeUp.Axe_Player.activeSelf || triggerAxeUp.Axe_Player.activeInHierarchy))
        {
            _animator.SetTrigger("AttackTr_Axe");
            //_animator.ResetTrigger("AttackTr_Axe");
        }
        else
        {
            _animator.SetTrigger("AttackTr");
            //_animator.ResetTrigger("AttackTr");
        }
            
    }
    void Block()
    {        
        // if(triggerSwordUp.Sword_Player != null && (triggerSwordUp.Sword_Player.activeSelf || triggerSwordUp.Sword_Player.activeInHierarchy))
        // {
            //_animator.SetBool("Block", true);     
            _animator.SetBool("Block", true);  
            PlayerManager.isBlocking = true;        
        // }
    }
    void StopBlock()
    {             
        _animator.SetBool("Block", false);   
        PlayerManager.isBlocking = false;       
    }
    // void AIM()
    // {        
    //     //_animator.SetBool("Block", true);     
    //     _animator.SetBool("AimBool", true);           
    // }
    // void StopAIM()
    // {        
    //     //_animator.SetBool("Block", true);     
    //     _animator.SetBool("AimBool", false);           
    // }
}
