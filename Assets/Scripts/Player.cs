using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    
    private CharacterController characterController;
    

    private Vector3 SpawnBrickPosition;
    private Vector3 MoveDirection;
    private Vector3 LookDirection;
    private Vector3 MoveVector;
    private Vector3 StairMoveDirection;

    public int ID = 0;

    private float ySpeed;
    public float rotationSpeed;
    private float originalStepOffset;



    public LayerMask Ground;
    public override void Start()
    {
        base.Start();
        GetDownSlopeComponent();
    }
    public void Setup(FloatingJoystick floatingJoystick)
    {
        joystick = floatingJoystick;
    }


    public override void Update()
    {
        base.Update();
        
        Move();
   
    }

    public void Move()
    {      
        MoveDownSlope();
        rb.angularVelocity = Vector3.zero;
    }
    private void GetDownSlopeComponent()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }
    private void MoveDownSlope()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * Speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (IsGrounded())
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity = AdjustVelocityToSlope(velocity);
        velocity.y += ySpeed;

        if (Physics.Raycast(RayCastOrigin(), Vector3.down, out hit, 4f, ~(1 << 8)))
        {
            if (!hit.collider.CompareTag("Untagged"))
            {
                characterController.Move(velocity * Time.deltaTime);

            }

        }

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }

        return velocity;
    }

    private void GetStairMoveDirection()
    {
        StairMoveDirection = Vector3.ProjectOnPlane(MoveVector, hit.normal);
    }

    private bool IsGrounded()
    {

        if (Physics.CheckSphere(GroundCheck.position, 0.2f, Ground))
        {
            return true;

        }
        return false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            UIManager.Ins.OpenUI<Win>(UIManager.UIID.Win);

        }
    }
}

