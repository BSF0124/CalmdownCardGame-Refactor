using Core;
using System.Collections;

namespace Managers
{
    /// 화면 페이드 인/아웃 기능을 제공하는 매니저 인터페이스
    public interface IFadeManager : IManager
    {
        // 페이드 아웃
        IEnumerator FadeOut(float duration);

        // 페이드 인
        IEnumerator FadeIn(float duration);
    }
}