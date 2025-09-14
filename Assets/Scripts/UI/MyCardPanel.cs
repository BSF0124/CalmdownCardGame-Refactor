using System.Linq;
using Core;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MyCardPanel : MonoBehaviour
    {
        [SerializeField] private DeckBuilderPanel deckBuilder;

        [Header("UI Objects")]
        [SerializeField] private Button closeButton;
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject cardEntryPrefab;
        [SerializeField] private GameObject cardPackPanel;
        [SerializeField] private GameObject deckBuilderPanel;

        private ICardDataManager cardDataMgr;
        private IPlayerDataManager playerDataMgr;

        void Awake()
        {
            cardDataMgr = CoreManager.I.GetManager<ICardDataManager>();
            playerDataMgr = CoreManager.I.GetManager<IPlayerDataManager>();

            if (cardDataMgr == null || playerDataMgr == null)
                Debug.LogError("[MyCardPanel] Manager not found.");

            closeButton.onClick.AddListener(ClosePanel);
        }

        void OnEnable()
        {
            UpdateList();
            if (CoreManager.I.GetManager<IGameManager>().isStageSelected)
            {
                cardPackPanel.SetActive(false);
                deckBuilderPanel.SetActive(true);
            }
            else
            {
                cardPackPanel.SetActive(true);
                deckBuilderPanel.SetActive(false);
            }
        }

        public void ClosePanel()
        {
            CoreManager.I.GetManager<IGameManager>().isStageSelected = false;
            gameObject.SetActive(false);
        }

        private void UpdateList()
        {
            foreach (Transform child in content)
                Destroy(child.gameObject);

            var owned = playerDataMgr.OwnedCardCounts
                            .Where(kv => kv.Value > 0)
                            .OrderBy(kv => kv.Key);

            foreach (var kv in owned)
            {
                var cd = cardDataMgr.GetCard(kv.Key);
                var entry = Instantiate(cardEntryPrefab, content);
                entry.GetComponent<CardEntry>().Init(cd.cardSprite, kv.Value);
            }
        }

        public void OnCardEntryClicked(int cardID)
        {
            if (!CoreManager.I.GetManager<GameManager>().isStageSelected)
                return;

            var cd = cardDataMgr.GetCard(cardID);
            deckBuilder.AddCardList(cd);

            playerDataMgr.AddCard(cardID, -1);
            UpdateList();
        }
    }
}