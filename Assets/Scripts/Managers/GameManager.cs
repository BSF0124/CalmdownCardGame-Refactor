using Core;
using UnityEngine;

namespace Managers
{
    [DefaultExecutionOrder(-60)]
    public class GameManager : MonoBehaviour, IGameManager
    {
        public StageData CurrentStageData { get; set; }

        public bool isStageSelected { get; set; }

        void Awake()
        {
            CoreManager.I.RegisterManager<IGameManager>(this);

            isStageSelected = false;
        }
    }
}