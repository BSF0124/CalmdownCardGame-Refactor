using System;
using System.Collections.Generic;
using System.IO;
using Core;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(-20)]
    public class PlayerDataManager : MonoBehaviour, IPlayerDataManager
    {
        private class PlayerData
        {
            public int highestClearedStage;
            public int cardPackCount;
            public List<CardCount> ownedCards = new List<CardCount>();
        }

        [SerializeField]
        private class CardCount
        {
            public int id;
            public int count;
        }

        private const string DATA_FILE = "playerdata.json";
        private PlayerData _data;
        private Dictionary<int, int> _cardDict = new Dictionary<int, int>();

        public int HighestClearedStage
        {
            get => _data.highestClearedStage;
            set { _data.highestClearedStage = Mathf.Max(_data.highestClearedStage, value); }
        }

        public int CardPackCount
        {
            get => _data.cardPackCount;
            set => _data.cardPackCount = Mathf.Max(0, value);
        }

        public IReadOnlyDictionary<int, int> OwnedCardCounts => _cardDict;

        void Awake()
        {
            CoreManager.I.RegisterManager<IPlayerDataManager>(this);
            LoadPlayerData();
        }

        public void AddCard(int cardID, int count = 1)
        {
            if (_cardDict.ContainsKey(cardID))
                _cardDict[cardID] += count;
            else
                _cardDict[cardID] = count;
        }

        public int GetCardCount(int cardID)
            => _cardDict.TryGetValue(cardID, out var c) ? c : 0;

        // 플레이어 데이터 불러오기
        public void LoadPlayerData()
        {
            var path = Path.Combine(Application.persistentDataPath, DATA_FILE);
            if (File.Exists(path))
            {
                try
                {
                    var json = File.ReadAllText(path);
                    _data = JsonUtility.FromJson<PlayerData>(json);
                }
                catch
                {
                    Debug.LogWarning("[PlayerDataManager] Data parsing failed.");
                    _data = new PlayerData();
                }
            }
            else
            {
                _data = new PlayerData();
            }

            _cardDict.Clear();
            foreach (var c in _data.ownedCards)
                _cardDict[c.id] = c.count;
        }

        // 플레이어 데이터 저장
        public void SavePlayerData()
        {
            _data.ownedCards = new List<CardCount>();
            foreach (var kv in _cardDict)
                _data.ownedCards.Add(new CardCount { id = kv.Key, count = kv.Value });

            var json = JsonUtility.ToJson(_data, true);
            var path = Path.Combine(Application.persistentDataPath, DATA_FILE);

            try
            {
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[PlayerDataManager] Save Failed: {ex.Message}");
            }
        }
    }
}