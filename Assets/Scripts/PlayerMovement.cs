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
    [SerializeField] private bool isGrounded = true;

    // [Header ("Pogo Jump Settings")]
    // [SerializeField] private Transform pogoCheck;
    // [SerializeField] private float pogoCheckRadius = 0.3f;
    // [SerializeField] private LayerMask pogoLayer;
    // [SerializeField] private float pogoForce = 0.2f;
    // [SerializeField] private float pogoEffectDuration = 7f;
    // [SerializeField] private GameObject pogoEffectObject;
    // [SerializeField] private float pogoCooldown = 0.5f;
    // private float lastPogoTime = -999f;
    // bool isPogoPressed = true;


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

        // if (!isGrounded && isPogoPressed)
        // {
        //     StartCoroutine(PogoEffectCoroutine());
        //     Collider2D hit = Physics2D.OverlapCircle(pogoCheck.position, pogoCheckRadius, pogoLayer);

        //     if (hit != null)
        //     {
        //         PogoBounce();
        //     }

        //     isPogoPressed = false;
        // }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            Debug.Log("Jumping");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset vertical velocity before jumping
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            // isGrounded = false;
        }
    }

    // public void OnSpecialMechanicInput(InputAction.CallbackContext context)
    // {
    //     if (context.performed && Time.time >= lastPogoTime + pogoCooldown)
    //     {
    //         // Debug.Log("pogo");
    //         isPogoPressed = true;
    //         lastPogoTime = Time.time;
    //     }
    // }

    // void PogoBounce()
    // {
    //     Debug.Log("pogo jump action");
    //     rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reset jatuh
    //     rb.AddForce(Vector2.up * pogoForce, ForceMode2D.Impulse);

    //     // StartCoroutine(PogoEffectCoroutine());
    // }

    // IEnumerator PogoEffectCoroutine()
    // {
    //     pogoEffectObject.SetActive(true);
    //     yield return new WaitForSeconds(pogoEffectDuration);
    //     pogoEffectObject.SetActive(false);
    // }

    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         isGrounded = true;
    //         // Debug.Log("Player is grounded.");
    //     }
    // }

    // void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground") && isActiveAndEnabled)
    //     {
    //         StartCoroutine(CheckGroundedAfterDelay());
    //     }
    // }

    // IEnumerator CheckGroundedAfterDelay()
    // {
    //     yield return new WaitForSeconds(delayGroundCheck);

    //     isGrounded = false;
    //     // Debug.Log("Player is not grounded.");
    // }

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

        // Gizmos.color = Color.blue;
        // if (pogoCheck != null)
        //     Gizmos.DrawWireSphere(pogoCheck.position, pogoCheckRadius);
    }

    
}
