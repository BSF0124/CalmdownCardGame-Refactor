using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    public List<CardData> allCards { get; private set; }

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadAllCardData();
    }

    private void LoadAllCardData()
    {
        allCards = new List<CardData>(Resources.LoadAll<CardData>("CardData"));
        Debug.Log($"[DataManager] Loaded {allCards.Count} cards.");
    }

    public CardData GetCardByID(int cardID)
    {
        return allCards.Find(c => c.id == cardID);
    }
}