using System.Collections.Generic;
using System.Linq;
using _Project.Core;
using UnityEngine;

namespace _Project.Framework
{
    public class GameRegistry : IGameRegistry
    {
        private readonly GameRegistryAsset _registryAsset;

        private readonly IEventBus _eventBus;

        private readonly Dictionary<string, IMiniGameInfo> _registeredGames;
        public IReadOnlyList<IMiniGameInfo> RegisteredGames => _registryAsset.Games.ToList();

        public GameRegistry(IEventBus eventBus, GameRegistryAsset registryAsset)
        {
            _eventBus = eventBus;
            _registryAsset = registryAsset;

            _registeredGames = _registryAsset.Games.ToDictionary<IMiniGameInfo, string>(entry => entry.GameId);
        }

        public bool IsGameRegistered(string gameId)
        {
            return !string.IsNullOrEmpty(gameId) && RegisteredGames.Any(info => info.GameId == gameId);
        }

        public IMiniGameInfo GetGameInfo(string gameId)
        {
            if (string.IsNullOrEmpty(gameId))
            {
                Debug.LogError("[GameRegistry] Game ID cannot be null or empty");
                return null;
            }

            var gameInfo = RegisteredGames.FirstOrDefault(info => info.GameId == gameId);
            if (gameInfo != null)
            {
                return gameInfo;
            }

            Debug.LogWarning($"[GameRegistry] Game not found: {gameId}");
            return null;
        }

        public IReadOnlyList<IMiniGameInfo> GetGamesByCategory(GameCategory category)
        {
            return _registeredGames.Values
                .Where(game => game.Category == category)
                .ToList();
        }

        public void RegisterGame(IMiniGameInfo gameInfo)
        {
            if (gameInfo == null)
            {
                Debug.LogError("[GameRegistry] Cannot register null game info");
                return;
            }

            if (string.IsNullOrEmpty(gameInfo.GameId))
            {
                Debug.LogError("[GameRegistry] Cannot register game with null or empty ID");
                return;
            }

            if (_registeredGames.ContainsKey(gameInfo.GameId))
            {
                Debug.LogWarning($"[GameRegistry] Game already registered: {gameInfo.GameId}. Updating registration.");
            }

            _registeredGames[gameInfo.GameId] = gameInfo;
            Debug.Log($"[GameRegistry] Game registered: {gameInfo.GameId} ({gameInfo.DisplayName})");
        }

        public void UnregisterGame(string gameId)
        {
            if (string.IsNullOrEmpty(gameId))
            {
                Debug.LogError("[GameRegistry] Game ID cannot be null or empty");
                return;
            }

            if (_registeredGames.Remove(gameId))
            {
                Debug.Log($"[GameRegistry] Game unregistered: {gameId}");
            }
            else
            {
                Debug.LogWarning($"[GameRegistry] Game not found for unregistration: {gameId}");
            }
        }

        public void Clear()
        {
            var count = _registeredGames.Count;
            _registeredGames.Clear();
            Debug.Log($"[GameRegistry] Cleared {count} registered games");
        }
    }
}