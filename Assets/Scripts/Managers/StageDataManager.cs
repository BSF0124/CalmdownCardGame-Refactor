using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(-90)]
    public class StageDataManager : MonoBehaviour, IStageDataManager
    {
        private Dictionary<int, StageData> _stageMap;

        void Awake()
        {
            CoreManager.I.RegisterManager<IStageDataManager>(this);

            var assets = Resources.LoadAll<StageData>("StageData");
            _stageMap = assets.ToDictionary(sd => sd.stageNumber);

            Debug.Log($"[StageDataManager] Loaded {_stageMap.Count} stages");
        }

        public StageData GetStageData(int stageNumber)
        {
            if (_stageMap.TryGetValue(stageNumber, out var sd))
                return sd;
            
            Debug.LogError($"[StageDataManager] StageData {stageNumber} not found!");
            return null;
        }
    }
}