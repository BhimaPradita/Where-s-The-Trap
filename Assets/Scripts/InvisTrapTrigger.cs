using UnityEngine;

public class InvisTrapTrigger : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private Rigidbody2D trap;
    [SerializeField] private Vector2 trapMovePosition;
    [SerializeField] private float trapSpeed = 2f;
    
    private bool moveTrap = false;
    private bool isPressed = false;


    void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            if(isPressed == true)
            {
                return;
            }

            Debug.Log("Pressure plate activated!");
            trapMovePosition = new Vector2(trapMovePosition.x, trapMovePosition.y);

            moveTrap = true;

            isPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (moveTrap == true)
        {
            Vector2 newPosTrap = Vector2.MoveTowards(trap.position, trapMovePosition, trapSpeed * Time.fixedDeltaTime);

            trap.MovePosition(newPosTrap);

            if (Vector2.Distance(trap.position, trapMovePosition) < 0.01f)
            {
                moveTrap = false;
            }
        }
    }
}
