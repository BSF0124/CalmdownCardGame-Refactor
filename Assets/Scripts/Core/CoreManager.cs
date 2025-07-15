using UnityEngine;

namespace Core
{
    public class CoreManager : MonoSingleton<CoreManager>
    {
        public GameManager game { get; private set; }
        public DeckManager deck { get; private set; }

        private void Start()
        {

        }

        private void Init()
        {
            game = FindAnyObjectByType<GameManager>();
            deck = FindAnyObjectByType<DeckManager>();
        }
    }
}