using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    public float speed = 2f;
    public float rotationspeed = 10f;
    public Transform groundCheckerTransform;
    public LayerMask notPlayerMask;
    public float jumpForce = 5f;
    // Start is called before the first frame update
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

        if(directionVector.magnitude > Mathf.Abs(0.05f))
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationspeed);

        _animator.SetFloat("speed", Vector3.ClampMagnitude(directionVector, 1).magnitude); 

        Vector3 moveDir = Vector3.ClampMagnitude(directionVector, 1) * speed;   
        _rigidbody.velocity = new Vector3(moveDir.x, _rigidbody.velocity.y, moveDir.z);
        _rigidbody.angularVelocity = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Physics.CheckSphere(groundCheckerTransform.position, 0.3f, notPlayerMask))
        {
            _animator.SetBool("isinair", false);
        }
        else
        {
            _animator.SetBool("isinair", true);
        }
    }

    void Jump()
    {
        
        if(Physics.Raycast(groundCheckerTransform.position, Vector3.down, 0.2f, notPlayerMask))
        {
            _animator.SetTrigger("jump");
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}
