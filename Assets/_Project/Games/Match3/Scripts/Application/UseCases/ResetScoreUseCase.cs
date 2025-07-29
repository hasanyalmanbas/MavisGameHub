using _Project.Games.Match3.Scripts.Domain.Services;

namespace _Project.Games.Match3.Scripts.Application.UseCases
{
    public class ResetScoreUseCase
    {
        private readonly ScoreService _scoreService;

        public ResetScoreUseCase(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Execute()
        {
            _scoreService.Reset();
        }
    }
}