using Core;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DeckBuilderPanel : MonoBehaviour
    {
        [Header("UI Objects")]
        [SerializeField] private Button submitButton;
        [SerializeField] private RectTransform content;
        [SerializeField] private GameObject cardListPrefab;

        private ICardDataManager cardDataMgr;
        private IPlayerDataManager playerDataMgr;

        void Awake()
        {
            cardDataMgr = CoreManager.I.GetManager<ICardDataManager>();
            playerDataMgr = CoreManager.I.GetManager<IPlayerDataManager>();

            if (cardDataMgr == null || playerDataMgr == null)
                Debug.LogError("[MyCardPanel] Manager not found.");
        }

        void AddCardList(int cardID)
        {

        }

        void OnCardListClick()
        {
            
        }
    }
}