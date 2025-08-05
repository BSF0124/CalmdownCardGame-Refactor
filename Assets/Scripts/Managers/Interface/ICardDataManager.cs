using System.Collections.Generic;
using Core;

namespace Managers
{
    public interface ICardDataManager : IManager
    {
        IReadOnlyDictionary<int, CardData> AllCards { get; }

        CardData GetCard(int id);
    }
}