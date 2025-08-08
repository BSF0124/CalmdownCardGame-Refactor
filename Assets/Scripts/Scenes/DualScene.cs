using Core;
using Managers;
using UnityEngine;

public class DualScene : MonoBehaviour
{
    private IGameManager gameMgr;

    void Awake()
    {
        gameMgr = CoreManager.I.GetManager<IGameManager>();

        StageData stageData = gameMgr.CurrentStageData;
        print($"Stage Number: {stageData.stageNumber}");
        print($"Stage Mode: {stageData.mode}");
        print($"SpecialRules: {stageData.specialRules.Count}");
        foreach (var card in stageData.aiDeckIds)
        {
            print(card);
        }
    }
}
