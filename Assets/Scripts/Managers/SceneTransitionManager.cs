using UnityEngine;
using UnityEngine.SceneManagement;
using Core;
using System.Collections;

namespace Managers
{
    [DefaultExecutionOrder(-40)]
    public class SceneTransitionManager : MonoBehaviour, ISceneTransitionManager
    {
        private IFadeManager _fadeMgr;
        private Enums.SceneType _currentScene = Enums.SceneType.MainMenu;
        private bool isCoroutineRunning = false;

        [SerializeField] private float fadeDuration = 0.5f;

        void Awake()
        {
            if (CoreManager.I == null)
            {
                Debug.LogError("[SceneTransitionManager] CoreManager not found.");
                return;
            }

            CoreManager.I.RegisterManager<ISceneTransitionManager>(this);

            _fadeMgr = CoreManager.I.GetManager<IFadeManager>();
            if (_fadeMgr == null)
                Debug.LogError("[SceneTransitionManager] IFadeManager not found.");

            SceneManager.LoadSceneAsync((int)_currentScene, LoadSceneMode.Additive);
        }

        // 씬 전환
        public void LoadScene(Enums.SceneType sceneType)
        {
            if (!isCoroutineRunning)
                StartCoroutine(TransitionScene(sceneType));
        }

        public IEnumerator TransitionScene(Enums.SceneType sceneType)
        {
            isCoroutineRunning = true;

            // 1) 페이드 아웃
            yield return StartCoroutine(_fadeMgr.FadeOut(fadeDuration));

            // 2) 현재 씬 언로드
            var unloadOp = SceneManager.UnloadSceneAsync((int)_currentScene);
            yield return unloadOp;

            // 3) 다음 씬 로드
            var loadOp = SceneManager.LoadSceneAsync((int)sceneType, LoadSceneMode.Additive);
            yield return loadOp;
            _currentScene = sceneType;

            // 4) 페이드 인
            yield return StartCoroutine(_fadeMgr.FadeIn(fadeDuration));

            isCoroutineRunning = false;
        }
    }
}