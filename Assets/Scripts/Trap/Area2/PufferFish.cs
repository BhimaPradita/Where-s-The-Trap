using UnityEngine;
using System.Collections;

public class PufferFish : MonoBehaviour
{
    [Header("Puffer Settings")]
    [SerializeField] private float puffInterval = 2f; // jeda sebelum membesar lagi
    [SerializeField] private float puffDuration = 0.5f; // durasi membesar/mengecil
    [SerializeField] private float stayPuffedDuration = 1f; // lama diam saat membesar
    [SerializeField] private float maxScaleMultiplier = 1.5f; // ukuran maksimum

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale * maxScaleMultiplier;

        StartCoroutine(PuffRoutine());
    }

    private IEnumerator PuffRoutine()
    {
        while (true)
        {
            // Tunggu sebelum membesar
            yield return new WaitForSeconds(puffInterval);

            // Membesar
            yield return StartCoroutine(
                ScaleOverTime(originalScale, targetScale)
            );

            // Diam sebentar saat membesar
            yield return new WaitForSeconds(stayPuffedDuration);

            // Mengecil
            yield return StartCoroutine(
                ScaleOverTime(targetScale, originalScale)
            );
        }
    }

    private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale)
    {
        float time = 0f;

        while (time < puffDuration)
        {
            transform.localScale = Vector3.Lerp(
                startScale,
                endScale,
                time / puffDuration
            );

            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
    }
}