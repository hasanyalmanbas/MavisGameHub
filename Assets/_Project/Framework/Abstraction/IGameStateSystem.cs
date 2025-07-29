using System;

namespace _Project.Framework
{
    public interface IGameStateSystem
    {
        void RegisterStateManager<TState>(StateManager<TState> manager) where TState : Enum;
        StateManager<TState> GetStateManager<TState>() where TState : Enum;
    }
}