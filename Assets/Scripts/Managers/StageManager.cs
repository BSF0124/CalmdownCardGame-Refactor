using UnityEngine;

public class StageManager : MonoSingleton<StageManager>
{
    public int highestClearedStage { get; private set; } = 0;

    // 스테이지 클리어 시 호출
    public void SetStageCleared(int stageID)
    {
        highestClearedStage = Mathf.Max(highestClearedStage, stageID);
    }

    // 해당 스테이지를 플레이할 수 있는지 확인
    public bool CanPlayStage(int stageID)
    {
        return stageID <= highestClearedStage + 1;
    }

    // 세이브 데이터 로드 시 호출
    public void LoadFromSave(int highestStage)
    {
        highestClearedStage = highestStage;
    }
}