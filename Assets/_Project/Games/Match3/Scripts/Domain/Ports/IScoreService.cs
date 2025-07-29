using System;

namespace _Project.Games.Match3.Scripts.Domain.Ports
{
    public interface IScoreService
    {
        int Score { get; }
        void Reset();
        void Add(int amount);
    }
}