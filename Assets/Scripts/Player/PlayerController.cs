using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;


/// <summary>
/// This script will handle the player movement and rotation
/// </summary>


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [Header("Movement and Input")]
    public float speed = 5f, gravity = 10f, jumpForce = 10f, turnSpeed = 15f;
    private Vector3 _velocity;

    [HideInInspector] public Vector3 _input, _camEuler, _targetPos, _moveDir;
    private Camera _camera;
    private Transform _cameraTransform;
    [SerializeField] private Animator _animator;
    [HideInInspector] public CharacterController _controller;
    private PlayerAudioController _audioController;
    private bool _isPlayerFrozen;

    [Header("Jumping")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    
    //updated to public for audio script
    [HideInInspector] public bool _isGrounded;
    public Material currentMat;
    

    [Header("Required Player Objects")] 
    public Transform playerPushPosition; //This has to be public

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        _audioController = GetComponent<PlayerAudioController>();

        if (_controller == null)
        {
            Debug.LogError("Character Controller not found!");
        }
        _camera = Camera.main;
        _animator = GetComponentInChildren<Animator>();

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
        if (!_isPlayerFrozen) { HandleMovement(); HandleJumping(); _controller.enabled = true; }
        else                  { _controller.enabled = false; }
    }

    void FixedUpdate()
    {
        HandleAnim();
    }

    void HandleMovement()
    {
            _isGrounded = _controller.isGrounded;
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
        

        
            //Get the input from the user
            _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
            _camEuler = _camera.transform.eulerAngles;

            //Calculate the angle to rotate the player
            Transform cameraTransform = _camera.transform;
            _moveDir = (cameraTransform.forward * _input.z + cameraTransform.right * _input.x).normalized;
            _moveDir.y = 0;
        
            if (_input.magnitude >= 0.1f) 
            {
                Quaternion targetRot = Quaternion.LookRotation(_moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
                
                _controller.Move(_moveDir * speed * Time.deltaTime);        
            }
    }

    void HandleAnim()
    {
        _animator.SetFloat("Velocity", _input.magnitude);
    }


    void HandleJumping()
    {
        //Gravity
        _velocity.y -= gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
        
        //Handle jumping
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _audioController.PlayJump();
            _velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
    }

    public IEnumerator FreezePlayer(float duration)
    {
        if (duration != 0)
        {
            //Freeze player with time
            _isPlayerFrozen = true;

            yield return new WaitForSeconds(duration);

            _isPlayerFrozen = false;
        }
        else
        {
            //Freeze player until toggled off
            _isPlayerFrozen = true;
        }
    }

        
    

    void OnControllerColliderHit(ControllerColliderHit collision)
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
