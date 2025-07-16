using UnityEngine;

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

            SceneLoader.Instance.LoadScene("MainMenu", true);
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