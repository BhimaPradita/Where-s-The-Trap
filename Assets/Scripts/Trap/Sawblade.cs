using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    [Header("Movement Point")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;

    private Vector3 targetPoint;

    void Start()
    {
        // target awal ke point B
        targetPoint = pointB.position;
    }

    void Update()
    {
        // gerakkan trap menuju target
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPoint,
            moveSpeed * Time.deltaTime
        );

        // jika sudah sampai target
        if (Vector3.Distance(transform.position, targetPoint) < 0.05f)
        {
            // ganti target
            if (targetPoint == pointA.position)
            {
                targetPoint = pointB.position;
            }
            else
            {
                targetPoint = pointA.position;
            }
        }
    }

    void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.red;

            // garis jalur trap
            Gizmos.DrawLine(pointA.position, pointB.position);

            // titik point
            Gizmos.DrawSphere(pointA.position, 0.15f);
            Gizmos.DrawSphere(pointB.position, 0.15f);
        }
    }
}