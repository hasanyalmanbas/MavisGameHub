using System;
using System.Collections.Generic;

namespace _Project.Framework
{
    public class StateManager<TState> where TState : Enum
    {
        public TState CurrentState { get; private set; }
        public TState PreviousState { get; private set; }

        public event Action<TState, TState>? OnStateChanged;

        public void SetState(TState newState)
        {
            if (EqualityComparer<TState>.Default.Equals(CurrentState, newState))
                return;

            PreviousState = CurrentState;
            CurrentState = newState;
            OnStateChanged?.Invoke(PreviousState, CurrentState);
        }

        public bool IsInState(TState state)
        {
            return EqualityComparer<TState>.Default.Equals(CurrentState, state);
        }
    }
}