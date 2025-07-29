namespace _Project.Games.Match3.Domain.Ports
{
    public interface IScoreService
    {
        int Score { get; }
        void Reset();
        void Add(int amount);
    }
}