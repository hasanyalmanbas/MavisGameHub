using System.Collections.Generic;

namespace _Project.Framework
{
    public interface IGameRegistry
    {
        IReadOnlyList<IMiniGameInfo> RegisteredGames { get; }
        
        bool IsGameRegistered(string gameId);
        
        IMiniGameInfo GetGameInfo(string gameId);
        
        IReadOnlyList<IMiniGameInfo> GetGamesByCategory(GameCategory category);
        
        void RegisterGame(IMiniGameInfo gameInfo);

        void UnregisterGame(string gameId);

        void Clear();
    }
}