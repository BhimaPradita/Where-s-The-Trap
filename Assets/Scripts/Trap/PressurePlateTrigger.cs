using UnityEngine;

public class PressurePlateTrigger : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private Rigidbody2D door;
    [SerializeField] private Vector2 doorMovePosition;
    [SerializeField] private float doorSpeed = 2f;

    [Header("Plate Settings")]
    [SerializeField] private Rigidbody2D plate;
    [SerializeField] private Vector2 plateMovePosition;
    [SerializeField] private float plateSpeed = 2f;
    
    private bool moveDoor = false;
    private bool movePlate = false;
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
            doorMovePosition = new Vector2(doorMovePosition.x, doorMovePosition.y);
            plateMovePosition = new Vector2(plate.position.x, plateMovePosition.y);

            moveDoor = true;
            movePlate = true;

            isPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (moveDoor == true)
        {
            Vector2 newPosDoor = Vector2.MoveTowards(door.position, doorMovePosition, doorSpeed * Time.fixedDeltaTime);

            door.MovePosition(newPosDoor);

            if (Vector2.Distance(door.position, doorMovePosition) < 0.01f)
            {
                moveDoor = false;
            }
        }

        if (movePlate == true)
        {
            Vector2 newPosPlate = Vector2.MoveTowards(plate.position, plateMovePosition, plateSpeed * Time.fixedDeltaTime);
            plate.MovePosition(newPosPlate);

            if (Vector2.Distance(plate.position, plateMovePosition) < 0.01f)
            {
                movePlate = false;
            }
        }
    }
}
