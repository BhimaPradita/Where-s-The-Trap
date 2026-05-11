using UnityEngine;

public class SpikeFall : MonoBehaviour
{
    public Transform player;
    public float triggerDistance = 3f;

    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        if (!hasFallen)
        {
            float distance = Vector2.Distance(
                transform.position,
                player.position
            );

            if (distance <= triggerDistance)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                hasFallen = true;
            }
        }
    }
}