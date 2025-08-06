using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class StageDataManager : MonoBehaviour
    {
        private Dictionary<int, StageData> _stageMap;

        void Awake()
        {
            var assets = Resources.LoadAll<StageData>("StageData");
            _stageMap = new Dictionary<int, StageData>();
            foreach (var sd in assets) _stageMap[sd.stageNumber] = sd;
        }

        public StageData GetStage(int num) => _stageMap.TryGetValue(num, out var sd) ? sd : null;
    }
}