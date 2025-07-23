using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardInventory
{
    public int cardID;
    public int count;
}

[System.Serializable]
public class GameSaveData
{
    public int highestClearedStage = 0;
    public List<CardInventory> cardInventories = new List<CardInventory>();
    public int ownedCardPackCount = 0;

    [Header("Settings")]
    public float masterVolume = 1f;
    public float bgmVolume = 0.5f;
    public float sfxVolume = 0.5f;
    public bool isFullScreen = true;
    public int resolutionIndex = 0;
}
