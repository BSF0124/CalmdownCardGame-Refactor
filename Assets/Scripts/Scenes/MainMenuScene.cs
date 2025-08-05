using Core;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [Tooltip("게임 시작 버튼")]
    public Button startButton;
    [Tooltip("옵션 버튼")]
    public Button optionButton;
    [Tooltip("게임 종료 버튼")]
    public Button quitButton;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        startButton.onClick.AddListener(OnStartGame);
        optionButton.onClick.AddListener(OnOpenOptions);
        quitButton.onClick.AddListener(OnQuitGame);
    }

    private void OnStartGame()
    {
        CoreManager.I.GetManager<ISceneTransitionManager>().LoadScene(Enums.SceneType.WorldMap);
    }

    private void OnOpenOptions()
    {
        CoreManager.I.GetManager<IOptionManager>().ShowOptionPanel();
    }

    private void OnQuitGame()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
