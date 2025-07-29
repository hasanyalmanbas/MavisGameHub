using _Project.Games.Match3.Scripts.Domain.Services;

namespace _Project.Games.Match3.Scripts.Application.UseCases
{
    public class AddScoreUseCase
    {
        private readonly ScoreService _scoreService;

        public AddScoreUseCase(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Execute(int amount)
        {
            _scoreService.Add(amount);
        }
    }
}