using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("Fade Effect")]
    [SerializeField] private CanvasGroup fadeCanvas;

    [Header("UI Panel")]
    [SerializeField] private List<GameObject> uiPanels;
    private Dictionary<string, GameObject> panelDict = new Dictionary<string, GameObject>();

    protected override void Awake()
    {
        base.Awake();
        fadeCanvas.gameObject.SetActive(false);
        RegisterAllPanels();
    }

    #region Fade In/Out
    public IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < Define.FadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1f, 0f, elapsed / Define.FadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 0f;
        fadeCanvas.gameObject.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        fadeCanvas.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < Define.FadeDuration)
        {
            elapsed += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0f, 1f, elapsed / Define.FadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = 1f;
    }
    #endregion

    #region UI Panel
    public void RegisterAllPanels()
    {
        panelDict.Clear();
        foreach (var panel in uiPanels)
        {
            if (panel == null) continue;
            panelDict[panel.name] = panel;
            panel.SetActive(false);
        }
    }

    public void ShowPanel(string panelName)
    {
        if (panelDict.TryGetValue(panelName, out var panel))
            panel.SetActive(true);
        else
            Debug.LogWarning($"UIManager: Panel '{panelName}' not found.");
    }

    public void HidePanel(string panelName)
    {
        if (panelDict.TryGetValue(panelName, out var panel))
            panel.SetActive(false);
    }
    #endregion 
}
