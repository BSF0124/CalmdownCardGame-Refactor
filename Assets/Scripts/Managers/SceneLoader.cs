using System;
using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

// 비동기 씬 전환 관리
// 로딩 화면(UIManager)과 연동하여 프로그레스 바 표시 및 완료 이벤트를 제공
public class SceneLoader : MonoSingleton<SceneLoader>
{
    // 씬 로딩 진행도를 구독할 때 사용
    public event Action<float> OnProgressChanged;
    // 씬 로딩 완료 후 호출
    public event Action OnSceneLoaded;

    private string currentScene = "MainMenuScene";
    private bool isLoading = false;

    // 지정한 씬으로 Additive 로드
    public void LoadScene(string sceneName, bool showLoadingUI = true, bool unloadPrev = true)
    {
        if (isLoading) return;
        StartCoroutine(LoadAsync(sceneName, showLoadingUI, unloadPrev));
    }

    private IEnumerator LoadAsync(string sceneName, bool showLoadingUI, bool unloadPrev)
    {
        isLoading = true;

        // 1) 페이드 아웃
        yield return StartCoroutine(CoreManager.Instance.ui.FadeEffect(0f, 1f));

        // 2) 로딩 화면 열기
        if (showLoadingUI)
            CoreManager.Instance.ui.ShowLoadingScreen();

        // 3) 이전 씬 언로드
        if (unloadPrev && !string.IsNullOrEmpty(currentScene))
            yield return SceneManager.UnloadSceneAsync(currentScene);

        // 4) 새 씬 Additive 로드
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        op.allowSceneActivation = true;

        while (!op.isDone)
        {
            OnProgressChanged?.Invoke(op.progress);
            yield return null;
        }
        currentScene = sceneName;

        if (showLoadingUI) CoreManager.Instance.ui.HideLoadingScreen();
        yield return StartCoroutine(CoreManager.Instance.ui.FadeEffect(1f, 0f));

        // OnProgressChanged?.Invoke(1.0f);
        // yield return new WaitForSeconds(0.5f);

        // // 4) 씬 활성화
        // op.allowSceneActivation = true;
        // while (!op.isDone)
        //     yield return null;

        // // 5) 로딩 화면 닫기
        // if (showLoadingUI)
        //     CoreManager.Instance.ui.HideLoadingScreen();

        // // 6) 페이드 인
        // yield return UIManager.Instance.FadeEffect(1f, 0f);

        // 7) 완료 콜백
        OnSceneLoaded?.Invoke();
        isLoading = false;
    }
}
