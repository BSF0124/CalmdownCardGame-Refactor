using System.Collections;
using Core;

namespace Managers
{
    public interface ISceneTransitionManager : IManager
    {
        void LoadScene(Enums.SceneType sceneType);

        IEnumerator TransitionScene(Enums.SceneType sceneType);
    }
}
