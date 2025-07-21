using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoSingleton<InventoryManager>
{
    private Dictionary<int, int> cardInventory = new Dictionary<int, int>();

    public int CardPackCount { get; private set; }

    // 카드 추가/제거
    public void AddCard(int cardID, int delta)
    {
        if (!cardInventory.ContainsKey(cardID))
            cardInventory[cardID] = 0;
        cardInventory[cardID] = Mathf.Max(0, cardInventory[cardID] + delta);
    }

    // 해당 카드의 현재 보유 개수 반환
    public int GetCardCount(int cardID)
    {
        return cardInventory.TryGetValue(cardID, out var c) ? c : 0;
    }

    // 카드팩 개수 설정
    public void SetCardPackCount(int count)
    {
        CardPackCount = Mathf.Max(0, count);
    }

    // 세이브용 List<CardInventory> 생성
    public List<CardInventory> GetInventoryList()
    {
        return cardInventory
            .Select(kvp => new CardInventory { cardID = kvp.Key, count = kvp.Value })
            .ToList();
    }

    // 로드된 세이브 데이터로 내부 딕셔너리 복원
    public void LoadInventory(List<CardInventory> list)
    {
        cardInventory.Clear();
        foreach (var item in list)
            cardInventory[item.cardID] = item.count;
    }
}