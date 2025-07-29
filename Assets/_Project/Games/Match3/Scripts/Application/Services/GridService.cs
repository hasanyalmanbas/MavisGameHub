using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Games.Match3.Domain.Entities;
using _Project.Games.Match3.Domain.Ports;

namespace _Project.Games.Match3.Application.Services
{
    public class GridService : IGridService
    {
        private System.Random _random = new();

        public Tile[,] GenerateGrid(int width, int height)
        {
            var grid = new Tile[width, height];

            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var type = (TileType)_random.Next(Enum.GetValues(typeof(TileType)).Length);
                grid[x, y] = new Tile(x, y, type);
            }

            return grid;
        }

        public List<Tile> GetMatches(Tile[,] grid)
        {
            var matches = new List<Tile>();
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            // Horizontal
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width - 2; x++)
            {
                var a = grid[x, y];
                var b = grid[x + 1, y];
                var c = grid[x + 2, y];

                if (a.Type == b.Type && b.Type == c.Type)
                {
                    matches.Add(a);
                    matches.Add(b);
                    matches.Add(c);
                }
            }

            // Vertical
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height - 2; y++)
            {
                var a = grid[x, y];
                var b = grid[x, y + 1];
                var c = grid[x, y + 2];

                if (a.Type == b.Type && b.Type == c.Type)
                {
                    matches.Add(a);
                    matches.Add(b);
                    matches.Add(c);
                }
            }

            return matches.Distinct().ToList();
        }

        public void RemoveMatches(Tile[,] grid, List<Tile> matches)
        {
            foreach (var tile in matches)
                grid[tile.X, tile.Y] = null;
        }

        public void DropTiles(Tile[,] grid)
        {
            int width = grid.GetLength(0);
            int height = grid.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                int dropTo = 0;
                for (int y = 0; y < height; y++)
                {
                    if (grid[x, y] != null)
                    {
                        if (y != dropTo)
                        {
                            grid[x, dropTo] = grid[x, y];
                            grid[x, dropTo].Y = dropTo;
                            grid[x, y] = null;
                        }

                        dropTo++;
                    }
                }
            }
        }
    }
}