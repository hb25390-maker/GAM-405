using JetBrains.Annotations;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class Playermovement : MonoBehaviour
{

    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference jumpAction;
    public Transform cameraTransform;

    public float mouseSensitivity = 2f;

    public float moveSpeed = 5f;

    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 lookInput;
    [SerializeField] float floorCheckDistance;
    [SerializeField] float jumpSpeed;
    [SerializeField] float gravity;

    float pitch;

    [SerializeField] private Rigidbody rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //this grabs the rigid body component 

    }
    void Update()
    {
        moveInput = moveAction != null ? moveAction.action.ReadValue<Vector2>() : Vector2.zero;
        lookInput = lookAction != null ? lookAction.action.ReadValue<Vector2>() : Vector2.zero;
        bool jumpInput = Input.GetKeyDown(KeyCode.Space);
        //bool jumpInput = jumpAction.action.started;

        if (cameraTransform) HandleLook();

        HandleMovement(jumpInput);
    }

    void OnEnable()
    {
        moveAction?.action.Enable();
        lookAction?.action.Enable();

    }
    void OnDisable()
    {
        moveAction?.action.Disable();
        lookAction?.action.Disable();

    }

    private void HandleMovement(bool jumpInput)
    {
        if(jumpInput)
        {
            Debug.Log("Jump input is working");
        }

        Vector3 inputDir = new Vector3(moveInput.x, 0f, moveInput.y);
        if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();

        Vector3 horizontalVelocity = transform.TransformDirection(inputDir) * moveSpeed;
        Vector3 verticalVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);

        if (IsGrounded())
        {
            if(verticalVelocity.y < 0)
            {
                verticalVelocity = Vector3.zero;
            }

            if(jumpInput)
            {
                verticalVelocity = Vector3.up * jumpSpeed;
            }
        }
        else
        {
            verticalVelocity -= Vector3.up * gravity * Time.deltaTime;
        }


        rb.linearVelocity = horizontalVelocity + verticalVelocity;
    }

    void HandleLook()
    {
        float yaw = lookInput.x * mouseSensitivity;
        transform.Rotate(Vector3.up * yaw, Space.World);

        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -80f, 80f);
        cameraTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);

    }
    private bool IsGrounded()
    {
        RaycastHit hitInfo;
        Physics.Raycast(this.transform.position, -transform.up, out hitInfo, floorCheckDistance);

        if(hitInfo.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}


