using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Animations;

/// <summary>
/// This script will handle the player movement and rotation
/// </summary>

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [Header("Movement and Input")]
    public float moveSpeed = 5f;
    public float jumpForce = 2f;
    public float gravity = 20f;
    public float rotationSpeed = 5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    
    private Camera _camera;
    private bool isPlayerFrozen;
    private Transform cameraTransform;

    [Header("Required Player Objects")] 
    public Transform playerPushPosition; //This has to be public

    [Header("Player Model and Animations")] 
    [SerializeField] private GameObject characterObject;
    private Animation _anim;

    void Start()
    {
        _camera = Camera.main;
        _anim = characterObject.GetComponent<Animation>();
        controller = GetComponent<CharacterController>();
        cameraTransform = _camera.transform;
        
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
        if (!isPlayerFrozen)
        {
            //Check if grounded
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            //Player input
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            //Input to the relative direction of the camera
            Vector3 moveDirection = (cameraTransform.forward * moveZ + cameraTransform.right * moveX).normalized;
            moveDirection.y = 0f;

            //Actually move
            if (moveDirection.magnitude >= 0.1f)
            {
                //Rotate
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                //Move
                controller.Move(moveDirection * moveSpeed * Time.deltaTime);

                ChangeAnimation("WalkAnim", 0.5f);
            }
            else
            {
                ChangeAnimation("IdleAnim", 0.25f);
            }

            //Jumping
            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
            }

            //Gravity
            velocity.y -= gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void ChangeAnimation(string animationName, float fadeTime)
    {
        _anim.CrossFade(animationName, fadeTime);
    }

    public IEnumerator FreezePlayer(float duration)
    {
        if (duration != 0)
        {
            //Freeze player with time
            isPlayerFrozen = true;

            yield return new WaitForSeconds(duration);

            isPlayerFrozen = false;
        }
        else
        {
            //Freeze player until toggled off
            isPlayerFrozen = true;
        }
    }

    public void UnfreezePlayer()
    {
        isPlayerFrozen = false;
    }
}
