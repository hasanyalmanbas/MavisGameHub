namespace _Project.Games.Match3.Scripts.Domain.Entities
{
    public enum TileType
    {
        Red,
        Green,
        Blue,
        Yellow,
        Purple
    }

    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public TileType Type { get; set; }

        public Tile(int x, int y, TileType type)
        {
            X = x;
            Y = y;
            Type = type;
        }
    }
}