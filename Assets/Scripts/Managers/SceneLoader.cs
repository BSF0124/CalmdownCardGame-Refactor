using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// 비동기 씬 전환 관리
public class SceneLoader : MonoSingleton<SceneLoader>
{
    // 씬 로딩 완료 후 호출
    public event Action OnSceneLoaded;
    private string currentScene = "MainMenu";
    private bool isLoading = false;

    // 지정한 씬으로 Additive 로드
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsync(sceneName));
    }

    private IEnumerator LoadAsync(string sceneName)
    {
        if (isLoading) yield break;

        isLoading = true;

        // 1) 페이드 아웃
        yield return StartCoroutine(CoreManager.Instance.ui.FadeOut());

        // 2) 이전 씬 언로드
        if(!string.IsNullOrEmpty(currentScene))
            yield return SceneManager.UnloadSceneAsync(currentScene);

        // 3) 새 씬 Additive 로드
        var loadOp = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        loadOp.allowSceneActivation = true;
        while (!loadOp.isDone)
            yield return null;
        currentScene = sceneName;
        yield return new WaitForSeconds(Define.FadeDuration/2f);

        // 4) 페이드 인
        yield return StartCoroutine(CoreManager.Instance.ui.FadeIn());

        // 5) 로드 완료
        OnSceneLoaded?.Invoke();

        isLoading = false;
    }
}