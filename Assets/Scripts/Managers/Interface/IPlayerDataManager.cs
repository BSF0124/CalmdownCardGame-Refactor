using System.Collections.Generic;
using Core;

namespace Managers
{
    public interface IPlayerDataManager : IManager
    {
        int HighestClearedStage { get; set; }
        int CardPackCount { get; set; }

        IReadOnlyDictionary<int, int> OwnedCardCounts { get; }

        void AddCard(int cardID, int count = 1);

        int GetCardCount(int cardID);

        void LoadPlayerData();

        void SavePlayerData();
    }
}
