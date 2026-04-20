using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image fadePanel;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1.0f;

    [Header("Level Settings")]
    [SerializeField] private string selectedLevelSceneName;

    private bool isTransitioning = false;
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        fadePanel.gameObject.SetActive(true);
        
        float elapsedTime = 0f;
        Color color = fadePanel.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            color.a = alpha;
            fadePanel.color = color;
            yield return null;
        }
        
        color.a = 0f;
        fadePanel.color = color;
        fadePanel.gameObject.SetActive(false);
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

    public void OnLevelButtonClicked()
    {
        StartCoroutine(FadeOutAndLoadScene(selectedLevelSceneName));
    }
}
