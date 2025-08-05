using Core;

namespace Managers
{
    public interface IOptionManager : IManager
    {
        void ShowOptionPanel();
        void HideOptionPanel();
        void LoadOption();
        void SaveOption();
    }
}
