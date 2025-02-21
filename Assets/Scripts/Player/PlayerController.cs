using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will handle the player movement and rotation
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [Header("Movement and Input")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = 10f;
    private float turnSpeed, currentAngle, currentAngleVelocity;
    
    private Rigidbody _rb;
    private Vector3 _input, camEuler;
    private Camera _camera;

    [Header("Jumping")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    private bool _isGrounded;

    [Header("Required Player Objects")] 
    public Transform playerPushPosition; //This has to be public

    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _camera = Camera.main;
        
        Instance = this;
        if (Instance != null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
    }

    void HandleMovement()
    {
        //Get the input from the user
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        camEuler = _camera.transform.eulerAngles;
        
        if (_input.magnitude >= 0.1f) 
        {
                
                //Calculate the angle to rotate the player
                float targetAngle = Mathf.Atan2(_input.x, _input.z) * Mathf.Rad2Deg + camEuler.y;
                
                currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, turnSpeed);
                transform.rotation = Quaternion.Euler(0, targetAngle, 0); //Look position

                //Actually move
                Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                transform.localPosition += speed * Time.deltaTime * moveDirection; 
        }

    }
    

    void HandleJumping()
    {
        //Check if the player is grounded
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask | LayerMask.GetMask("PuzzleBox"));
        
        if (!_isGrounded)
        {
            //Force the player onto the ground
            _rb.AddForce(Vector3.down * gravity);
        }
        
        //Handle jumping
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _rb.AddForce(Vector3.up * gravity, ForceMode.Impulse);
            _isGrounded = false;
        }
    }
}
