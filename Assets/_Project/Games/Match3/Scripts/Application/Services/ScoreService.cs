using _Project.Core;
using _Project.Games.Match3.Domain.Events;
using _Project.Games.Match3.Domain.Ports;

namespace _Project.Games.Match3.Application.Services
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