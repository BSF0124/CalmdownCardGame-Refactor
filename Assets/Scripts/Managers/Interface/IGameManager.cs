using Core;

namespace Managers
{
    public interface IGameManager : IManager
    {
        int SelectedStage { get; set; }
    }
}