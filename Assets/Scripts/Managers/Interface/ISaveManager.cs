using Core;

namespace Managers
{
    // 플레이어 데이터 세이브/로드 기능을 제공하는 매니저 인터페이스
    public interface ISaveManager : IManager
    {
        // 플레이어 데이터 로드
        void LoadGame();

        // 플레이어 데이터 세이브
        void SaveGame();
    }
}
