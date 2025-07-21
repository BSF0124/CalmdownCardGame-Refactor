using System.Collections.Generic;
using UnityEngine;


public class SaveManager : MonoSingleton<SaveManager>
{
    private const string SAVE_KEY = Define.GameSaveKey;
    public GameSaveData currentSave { get; private set; }

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadGame();
    }

    public void SaveGame()
    {
        // 1) 스테이지 정보
        currentSave.highestClearedStage = StageManager.Instance.highestClearedStage;

        // 2) 카드 인벤토리 정보
        currentSave.cardInventories = InventoryManager.Instance.GetInventoryList();

        // 3) 카드팩 개수
        currentSave.ownedCardPackCount = InventoryManager.Instance.CardPackCount;

        // 4) Json -> PlayerPrefs
        string json = JsonUtility.ToJson(currentSave);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("[SaveManager] Game saved.");
    }

    // 저장된 JSON이 있으면 로드, 없으면 빈 초기화
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            currentSave = JsonUtility.FromJson<GameSaveData>(json);
            Debug.Log("[SaveManager] Game loaded from save.");

            StageManager.Instance.LoadFromSave(currentSave.highestClearedStage);

            InventoryManager.Instance.LoadInventory(currentSave.cardInventories);
            InventoryManager.Instance.SetCardPackCount(currentSave.ownedCardPackCount);
        }
        else
        {
            currentSave = new GameSaveData();
            Debug.Log("[SaveManager] No save data; starting fresh.");
        }
    }
}