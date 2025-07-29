using System;
using System.Collections.Generic;

namespace _Project.Framework
{
    public class GameStateSystem : IGameStateSystem
    {
        private readonly Dictionary<Type, object> _managers = new();

        public void RegisterStateManager<TState>(StateManager<TState> manager) where TState : Enum
        {
            var type = typeof(TState);
            _managers.TryAdd(type, manager);
        }

        public StateManager<TState> GetStateManager<TState>() where TState : Enum
        {
            var type = typeof(TState);
            return _managers.TryGetValue(type, out var obj)
                ? obj as StateManager<TState>
                : null;
        }
    }
}