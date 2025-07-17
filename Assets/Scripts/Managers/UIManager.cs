using System.Collections;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Loading Scene")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private UnityEngine.UI.Slider progressBar;

    [Header("Fade Effect")]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration = 0.5f;

    #region Loading Screen
    public void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
        progressBar.value = 0f;

        // SceneLoader 이벤트 구독
        SceneLoader.Instance.OnProgressChanged += SetProgress;
    }

    public void HideLoadingScreen()
    {
        SceneLoader.Instance.OnProgressChanged -= SetProgress;
        loadingScreen.SetActive(false);
    }

    private void SetProgress(float p) => progressBar.value = p;
    #endregion

    #region Fade In/Out
    public IEnumerator FadeEffect(float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = end;
    }
    #endregion
}
