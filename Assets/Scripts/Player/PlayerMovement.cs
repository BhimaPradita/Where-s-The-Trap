using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;

    [Header ("Ground Check Settings")]
    // [SerializeField] private float delayGroundCheck = 0.1f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody2D rb;
    private Vector2 movementInput;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isGrounded = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if (movementInput != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else 
        {
            animator.SetBool("isRunning", false);
        }

        // Flip character
        if (movementInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else if (movementInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            animator.SetBool("isJump", true);
            Debug.Log("Jumping");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset vertical velocity before jumping
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
        {
            animator.SetBool("isJump", false);
        }
    }
    public Rigidbody2D GetRB()
    {
        return rb;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
