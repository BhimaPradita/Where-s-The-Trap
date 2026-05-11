using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwimming : MonoBehaviour
{
    [Header("Swim Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float swimUpForce = 5f;
    [SerializeField] private float swimDownForce = 3f;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private bool isSwimmingUp = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // pastikan gravity mati
        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        // Gerakan horizontal
        rb.linearVelocity = new Vector2(
            movementInput.x * moveSpeed,
            rb.linearVelocity.y
        );

        // Renang naik
        if (isSwimmingUp)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                swimUpForce
            );

            animator.SetBool("isSwimUp", true);
        }
        else if(isSwimmingUp == false && rb.linearVelocity.y > 0)
        {
            // Turun perlahan saat tombol dilepas
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                -swimDownForce
            );

            animator.SetBool("isSwimUp", false);
        }

        // Idle = true hanya saat player tidak bergerak
        bool isIdle =
            Mathf.Abs(rb.linearVelocity.x) < 0.1f &&
            Mathf.Abs(rb.linearVelocity.y) < 0.1f;

        animator.SetBool("isIdle", isIdle);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        // Running animation
        animator.SetBool("isSwimming", movementInput != Vector2.zero);

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

    public void OnSwimInput(InputAction.CallbackContext context)
    {
        // Saat tombol ditekan
        if (context.performed)
        {
            isSwimmingUp = true;
        }

        // Saat tombol dilepas
        if (context.canceled)
        {
            isSwimmingUp = false;
        }
    }
}