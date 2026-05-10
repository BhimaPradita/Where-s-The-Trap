using UnityEngine;

public class InvisTrapTrigger : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private Rigidbody2D trap;

    [Header("Movement Point")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private float trapSpeed = 2f;

    private bool moveTrap = false;
    private bool isPressed = false;

    private Vector2 targetPosition;

    void Start()
    {
        // set posisi awal trap ke point A
        trap.position = pointA.position;

        // target awal
        targetPosition = pointB.position;
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (isPressed)
            {
                return;
            }

            Debug.Log("Pressure plate activated!");

            moveTrap = true;
            isPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (moveTrap)
        {
            Vector2 newPosTrap = Vector2.MoveTowards(
                trap.position,
                targetPosition,
                trapSpeed * Time.fixedDeltaTime
            );

            trap.MovePosition(newPosTrap);

            // jika sudah sampai
            if (Vector2.Distance(trap.position, targetPosition) < 0.01f)
            {
                moveTrap = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawLine(pointA.position, pointB.position);

            Gizmos.DrawSphere(pointA.position, 0.15f);
            Gizmos.DrawSphere(pointB.position, 0.15f);
        }
    }
}