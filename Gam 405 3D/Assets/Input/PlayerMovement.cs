using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class Playermovement : MonoBehaviour
{

    public InputActionReference moveAction;

    public float moveSpeed = 5f;

    [SerializeField] private Vector2 moveInput;

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

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void OnEnable()
    {
        moveAction?.action.Enable();

    }
    void OnDisable()
    {
        moveAction?.action.Disable();

    }

    private void HandleMovement()
    {


        Vector3 inputDir = new Vector3(moveInput.x, 0f, moveInput.y);
        if (inputDir.sqrMagnitude > 1f) inputDir.Normalize();

        Vector3 move = transform.TransformDirection(inputDir) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position +  move);

    }
}
