using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Managers
{
    [DefaultExecutionOrder(-10)]
    public class CardDataManager : MonoBehaviour, ICardDataManager
    {
        private Dictionary<int, CardData> _cardDict;

        public IReadOnlyDictionary<int, CardData> AllCards => _cardDict;

        void Awake()
        {
            CoreManager.I.RegisterManager<ICardDataManager>(this);

            var assets = Resources.LoadAll<CardData>("CardData");

            _cardDict = new Dictionary<int, CardData>(assets.Length);
            foreach (var card in assets)
            {
                if (_cardDict.ContainsKey(card.id))
                {
                    Debug.LogWarning($"[CardDataManager] 중복된 id={card.id} ({card.name})");
                    continue;
                }
                _cardDict.Add(card.id, card);
            }

            Debug.Log($"[CardDataManager] Loaded Card: {_cardDict.Count}");
        }

        public CardData GetCard(int id)
        {
            _cardDict.TryGetValue(id, out var card);
            return card;
        }
    }
}