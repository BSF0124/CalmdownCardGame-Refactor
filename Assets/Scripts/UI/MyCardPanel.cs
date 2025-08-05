using System.Linq;
using Core;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MyCardPanel : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Button closeButton;
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject cardEntryPrefab;

        private ICardDataManager cardDataMgr;
        private IPlayerDataManager playerDataMgr;

        void Awake()
        {
            cardDataMgr = CoreManager.I.GetManager<ICardDataManager>();
            playerDataMgr = CoreManager.I.GetManager<IPlayerDataManager>();

            if (cardDataMgr == null || playerDataMgr == null)
                Debug.LogError("[MyCardPanel] Manager not found.");

            closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        public void OpenPanel()
        {
            UpdateList();
            gameObject.SetActive(true);
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
    }
}