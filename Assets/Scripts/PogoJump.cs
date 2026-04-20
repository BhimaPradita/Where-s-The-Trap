using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PogoJump : MonoBehaviour
{
    [Header("Reference")]
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    [Header("Pogo Settings")]
    [SerializeField] private Transform pogoCheck;
    [SerializeField] private float pogoCheckRadius = 0.3f;
    [SerializeField] private LayerMask pogoLayer;
    [SerializeField] private float pogoForce = 7f;

    [Header("Effect")]
    [SerializeField] private GameObject pogoEffectObject;
    [SerializeField] private float pogoEffectDuration = 0.5f;

    [Header("Cooldown")]
    [SerializeField] private float pogoCooldown = 0.5f;
    private float lastPogoTime = -999f;

    private bool isPogoPressed;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = playerMovement.GetRB();
    }

    void FixedUpdate()
    {
        if (!playerMovement.IsGrounded() && isPogoPressed)
        {
            Collider2D hit = Physics2D.OverlapCircle(pogoCheck.position, pogoCheckRadius, pogoLayer);

            if (hit != null)
            {
                PogoBounce();
            }

            isPogoPressed = false;
        }
    }

    public void OnSpecialMechanicInput(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= lastPogoTime + pogoCooldown)
        {
            isPogoPressed = true;
        }
    }

    void PogoBounce()
    {
        lastPogoTime = Time.time;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * pogoForce, ForceMode2D.Impulse);

        StartCoroutine(PogoEffectCoroutine());
    }

    IEnumerator PogoEffectCoroutine()
    {
        pogoEffectObject.SetActive(true);
        yield return new WaitForSeconds(pogoEffectDuration);
        pogoEffectObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (pogoCheck != null)
            Gizmos.DrawWireSphere(pogoCheck.position, pogoCheckRadius);
    }
}