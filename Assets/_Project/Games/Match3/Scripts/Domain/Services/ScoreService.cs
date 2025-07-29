using System;
using _Project.Core;
using _Project.Games.Match3.Scripts.Domain.Events;
using _Project.Games.Match3.Scripts.Domain.Ports;

namespace _Project.Games.Match3.Scripts.Domain.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IEventBus _eventBus;
        
        public ScoreService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public int Score { get; private set; }

        public void Reset()
        {
            Score = 0;
            _eventBus.Publish(new ScoreChangedEvent(Score));
        }

        public void Add(int amount)
        {
            Score += amount;
            _eventBus.Publish(new ScoreChangedEvent(Score));
         }
    }
}