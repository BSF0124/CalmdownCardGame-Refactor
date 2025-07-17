using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class CoreManager : MonoSingleton<CoreManager>
    {
        public GameManager game { get; private set; }
        public DeckManager deck { get; private set; }
        public UIManager ui { get; private set; }
        public SceneLoader scene { get; private set; }

        private void Start()
        {
            Init();
            SceneManager.LoadSceneAsync("MainMenuScene", LoadSceneMode.Additive);
            SceneLoader.Instance.LoadScene("GameScene", true);
        }

        private void Init()
        {
            game = FindAnyObjectByType<GameManager>();
            deck = FindAnyObjectByType<DeckManager>();
            ui = FindAnyObjectByType<UIManager>();
            scene = FindAnyObjectByType<SceneLoader>();
        }
    }
}