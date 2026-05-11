using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;

    public float minX;
    public float maxX;

    public float minY;
    public float maxY;

    void LateUpdate()
    {
        float targetX = Mathf.Clamp(
            player.position.x,
            minX,
            maxX
        );

        float targetY = Mathf.Clamp(
            player.position.y,
            minY,
            maxY
        );

        Vector3 targetPosition = new Vector3(
            targetX,
            targetY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}