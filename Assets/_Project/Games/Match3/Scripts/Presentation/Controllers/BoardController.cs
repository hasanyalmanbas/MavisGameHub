using System.Collections;
using System.Collections.Generic;
using _Project.Games.Match3.Application.UseCases;
using _Project.Games.Match3.Domain.Entities;
using _Project.Games.Match3.Domain.Ports;
using _Project.Games.Match3.Presentation.Views;
using Reflex.Attributes;
using UnityEngine;

namespace _Project.Games.Match3.Presentation.Controllers
{
    public class BoardController : MonoBehaviour
    {
        [Inject] private readonly IGridService _gridService;
        [Inject] private readonly IScoreService _scoreService;
        [Inject] private readonly AddScoreUseCase _addScoreUseCase;
        [Inject] private readonly ResetScoreUseCase _resetScoreUseCase;

        public int width = 8;
        public int height = 8;
        public GameObject tilePrefab;
        public Sprite[] tileSprites;

        private Tile[,] _grid;
        private TileView[,] _tileViews;

        private TileView _selectedTile;
        private bool _isBusy = false;

        private void Start()
        {
            _grid = _gridService.GenerateGrid(width, height);
            _tileViews = new TileView[width, height];
            CreateBoardVisual();
            StartCoroutine(CheckMatches());
        }

        private void CreateBoardVisual()
        {
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var tile = _grid[x, y];
                var view = Instantiate(tilePrefab, transform).GetComponent<TileView>();
                view.Init(tile, tileSprites[(int)tile.Type], this);
                view.SetPosition(new Vector2Int(x, y));
                _tileViews[x, y] = view;
            }
        }


        private IEnumerator CheckMatches()
        {
            if (_isBusy) yield break;
            _isBusy = true;

            var matches = _gridService.GetMatches(_grid);
            if (matches.Count == 0)
            {
                _isBusy = false;
                yield break;
            }

            var destroyTasks = new List<Coroutine>();
            foreach (var tile in matches)
            {
                var view = _tileViews[tile.X, tile.Y];
                if (view != null)
                {
                    destroyTasks.Add(StartCoroutine(view.PlayDestroyAnimation()));
                    _tileViews[tile.X, tile.Y] = null;
                }
            }

            yield return new WaitForSeconds(0.4f);

            _gridService.RemoveMatches(_grid, matches);
            _gridService.DropTiles(_grid);

            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var tile = _grid[x, y];
                if (tile != null && _tileViews[x, y] == null)
                {
                    var view = Instantiate(tilePrefab, transform).GetComponent<TileView>();
                    view.Init(tile, tileSprites[(int)tile.Type], this);
                    view.SetPosition(new Vector2Int(x, y), instant: false);
                    _tileViews[x, y] = view;
                }
                else if (tile != null)
                {
                    _tileViews[x, y].SetPosition(new Vector2Int(x, y), instant: false);
                }
            }

            yield return new WaitForSeconds(0.5f);
            _isBusy = false;
            CheckMatches();
        }

        private void UpdateBoardVisual()
        {
            for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var tile = _grid[x, y];
                if (tile != null && _tileViews[x, y] == null)
                {
                    var view = Instantiate(tilePrefab, transform).GetComponent<TileView>();
                    view.Init(tile, tileSprites[(int)tile.Type], this);
                    view.SetPosition(new Vector2Int(x, y));
                    _tileViews[x, y] = view;
                }
                else if (tile != null)
                {
                    _tileViews[x, y].SetPosition(new Vector2Int(x, y));
                }
            }
        }

        public void OnTileClicked(TileView clicked)
        {
            if (_selectedTile == null)
            {
                _selectedTile = clicked;
                _selectedTile.Highlight(true);
            }
            else
            {
                if (AreAdjacent(_selectedTile.TileData, clicked.TileData))
                {
                    TrySwap(_selectedTile, clicked);
                }

                _selectedTile.Highlight(false);
                _selectedTile = null;
            }
        }

        private bool AreAdjacent(Tile a, Tile b)
        {
            return (Mathf.Abs(a.X - b.X) == 1 && a.Y == b.Y) ||
                   (Mathf.Abs(a.Y - b.Y) == 1 && a.X == b.X);
        }

        private void TrySwap(TileView aView, TileView bView)
        {
            var a = aView.TileData;
            var b = bView.TileData;

            (_grid[a.X, a.Y], _grid[b.X, b.Y]) = (_grid[b.X, b.Y], _grid[a.X, a.Y]);
            (a.X, b.X) = (b.X, a.X);
            (a.Y, b.Y) = (b.Y, a.Y);

            aView.SetPosition(new Vector2Int(a.X, a.Y));
            bView.SetPosition(new Vector2Int(b.X, b.Y));
            (_tileViews[a.X, a.Y], _tileViews[b.X, b.Y]) = (aView, bView);

            var matches = _gridService.GetMatches(_grid);

            if (matches.Count > 0)
            {
                StartCoroutine(CheckMatches());
            }
            else
            {
                (_grid[a.X, a.Y], _grid[b.X, b.Y]) = (_grid[b.X, b.Y], _grid[a.X, a.Y]);
                (a.X, b.X) = (b.X, a.X);
                (a.Y, b.Y) = (b.Y, a.Y);

                aView.SetPosition(new Vector2Int(a.X, a.Y));
                bView.SetPosition(new Vector2Int(b.X, b.Y));
                (_tileViews[a.X, a.Y], _tileViews[b.X, b.Y]) = (aView, bView);
            }
        }
    }
}