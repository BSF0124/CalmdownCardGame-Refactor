using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    [DefaultExecutionOrder(-20)]
    public class FadeManager : MonoBehaviour, IFadeManager
    {
        [Tooltip("Fade Image")]
        [SerializeField] private Image fadeImage;

        void Awake()
        {
            if (fadeImage == null)
            {
                Debug.LogError("[FadeManager] fadeImage not found.");
                return;
            }
            if (CoreManager.I == null)
            {
                Debug.LogError("[FadeManager] CoreManager not found.");
                return;
            }

            CoreManager.I.RegisterManager<IFadeManager>(this);
            SetAlpha(0f);
        }

        public IEnumerator FadeOut(float duration)
        {
            fadeImage.gameObject.SetActive(true);
            // 1) 페이드 아웃
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                SetAlpha(Mathf.Clamp01(t / duration));
                yield return null;
            }
            SetAlpha(1f);
        }

        public IEnumerator FadeIn(float duration)
        {
            // 1) 페이드 아웃
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                SetAlpha(1f - Mathf.Clamp01(t / duration));
                yield return null;
            }
            SetAlpha(0f);
            fadeImage.gameObject.SetActive(false);
        }

        private void SetAlpha(float a)
        {
            var c = fadeImage.color;
            c.a = a;
            fadeImage.color = c;
        }
    }
}
