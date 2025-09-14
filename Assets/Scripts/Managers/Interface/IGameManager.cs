using Core;

namespace Managers
{
    public interface IGameManager : IManager
    {
        StageData CurrentStageData { get; set; }

        bool isStageSelected { get; set; }

        
    }
}