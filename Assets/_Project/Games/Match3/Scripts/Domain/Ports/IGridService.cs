using System.Collections.Generic;
using _Project.Games.Match3.Domain.Entities;

namespace _Project.Games.Match3.Domain.Ports
{
    public interface IGridService
    {
        Tile[,] GenerateGrid(int width, int height);
        List<Tile> GetMatches(Tile[,] grid);
        void RemoveMatches(Tile[,] grid, List<Tile> matches);
        void DropTiles(Tile[,] grid);
    }
}