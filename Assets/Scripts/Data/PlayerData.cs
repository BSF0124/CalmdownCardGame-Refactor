using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardInventory
{
    public int cardID;
    public int count;
}

[System.Serializable]
public class PlayerData
{
    public int hightestClearedStage = 0;
    public List<CardInventory> cardInventories = new List<CardInventory>();
    public int ownedCardPackCount = 0;
}
