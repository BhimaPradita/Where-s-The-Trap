using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        // Pastikan panel aktif
        fadePanel.gameObject.SetActive(true);

        Color color = fadePanel.color;

        // Mulai dari hitam penuh
        color.a = 1f;
        fadePanel.color = color;

        float elapsedTime = 0f;

        // Fade dari hitam -> transparan
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            color.a = alpha;
            fadePanel.color = color;

            yield return null;
        }

        // Pastikan benar-benar transparan
        color.a = 0f;
        fadePanel.color = color;

        // Optional: matikan panel setelah fade selesai
        fadePanel.gameObject.SetActive(false);
    }
}