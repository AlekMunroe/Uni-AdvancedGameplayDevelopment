using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    private float turnSpeed = 5, currentAngle, currentAngleVelocity;
    
    [HideInInspector] public Rigidbody _rb;
    [HideInInspector] public Vector3 _input, _camEuler, _targetPos, _moveDir;
    private Camera _camera;
    [SerializeField] private Animator _animator;
    private PlayerAudioController _audioController;

    [Header("Jumping")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    
    //updated to public for audio script
    [HideInInspector] public bool _isGrounded;
    public Material currentMat;
    

    [Header("Required Player Objects")] 
    public Transform playerPushPosition; //This has to be public

    void Start()
    {
        _audioController = GetComponent<PlayerAudioController>();
        _rb = this.GetComponent<Rigidbody>();
        _camera = Camera.main;
        _animator = this.GetComponentInChildren<Animator>();

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

    void FixedUpdate()
    {
        HandleAnim();
        GroundMat();
    }

    void HandleMovement()
    {
        //Get the input from the user
        _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        _camEuler = _camera.transform.eulerAngles;
        
        if (_input.magnitude >= 0.1f) 
        {
            
            //Calculate the angle to rotate the player
            float targetAngle = Mathf.Atan2(_input.x, _input.z) * Mathf.Rad2Deg + _camEuler.y;
                
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, turnSpeed);
            currentAngle = Mathf.Clamp(currentAngle, -90, 90);
            transform.rotation = Quaternion.Euler(0, targetAngle, 0); //Look position

            
            _moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        } 
        
        //Actually move
        if (_input.magnitude >= 0.1f || !_isGrounded)
        {
            transform.localPosition += speed * Time.deltaTime * _moveDir;
        }
        else if (_input.magnitude < 0.1f && _isGrounded)
        {
            _rb.velocity = Vector3.zero;
        }

    }

    void HandleAnim()
    {
        _animator.SetFloat("Velocity", _input.magnitude);
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
            _audioController.PlayJump();
            _rb.AddForce(Vector3.up * gravity, ForceMode.Impulse);
            _isGrounded = false;
        }
    }

    void GroundMat()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Renderer renderer = collision.collider.GetComponent<Renderer>();
        if (renderer != null)
            {
                currentMat = renderer.material;
            }
            else
            {
                Debug.Log("No renderer found!");
            }
        
    }
}
