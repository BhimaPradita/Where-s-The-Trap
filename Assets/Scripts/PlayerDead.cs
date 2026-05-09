using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    // [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverUI;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            Debug.Log("Player is dead!");
            gameOverUI.SetActive(true);
        }
    }

    void OnTriggeredEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            Debug.Log("Player is dead!");
            gameOverUI.SetActive(true);
        }
    }
}
