using Core;

namespace Managers
{
    public interface IStageDataManager : IManager
    {
        StageData GetStageData(int stageNumber);
    }
}