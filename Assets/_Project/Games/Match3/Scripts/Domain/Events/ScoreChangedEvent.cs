namespace _Project.Games.Match3.Scripts.Domain.Events
{
    public struct ScoreChangedEvent
    {
        public int NewScore;

        public ScoreChangedEvent(int score)
        {
            NewScore = score;
        }
    }
}