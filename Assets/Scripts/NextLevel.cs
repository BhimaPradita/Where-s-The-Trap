using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fadePanel;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.0f;
    [SerializeField] private Color fadeColor = Color.black;

    private bool isTransitioning = false;

    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Matikan movement player
            PlayerMovement movement = other.GetComponent<PlayerMovement>();

            if (movement != null)
            {
                movement.enabled = false;
            }

            // Hentikan velocity player
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0f;
            }

            Debug.Log("Loading next level: " + sceneName);
            StartCoroutine(FadeOutAndLoadScene(sceneName));
        }
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        if (isTransitioning) yield break;
        
        isTransitioning = true;
        
        // Aktifkan fade panel
        fadePanel.gameObject.SetActive(true);
        
        // Fade out ke blackscreen
        float elapsedTime = 0f;
        Color color = fadePanel.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            color.a = alpha;
            fadePanel.color = color;
            yield return null;
        }
        
        color.a = 1f;
        fadePanel.color = color;
        
        // Tunggu sebentar di blackscreen
        yield return new WaitForSeconds(0.2f);
        
        // Stop music dan load scene
        // StopMenuMusic();
        SceneManager.LoadScene(sceneName);
        
        isTransitioning = false;
    }
}
