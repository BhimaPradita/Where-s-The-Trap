using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Finish : MonoBehaviour
{
    [Header("Target Position")]
    [SerializeField] private Transform targetCenter;

    [Header("Y Offset")]
    [SerializeField] private float yOffset = 1f;

    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ada yang masuk trigger");

        // cek apakah yang masuk player
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player masuk finish");

            Transform player = collision.transform;

            // ambil Rigidbody player
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

            // matikan gravitasi
            if (rb != null)
            {
                rb.gravityScale = 0f;
                rb.linearVelocity = Vector2.zero;
            }

            // matikan semua collider player
            Collider2D[] colliders = player.GetComponents<Collider2D>();

            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }

            // ambil posisi target
            Vector3 targetPosition = targetCenter.position;

            // turunkan posisi Y
            targetPosition.y -= yOffset;

            // pindahkan player
            player.position = targetPosition;

            // ganti scene
            StartCoroutine(ChangeSceneCoroutine());
        }
    }

    IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(nextSceneName);
    }
}