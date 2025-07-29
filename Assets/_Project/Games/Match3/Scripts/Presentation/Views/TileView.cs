using System.Collections;
using _Project.Games.Match3.Scripts.Domain.Entities;
using _Project.Games.Match3.Scripts.Presentation.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Games.Match3.Scripts.Presentation.Views
{
    public class TileView : MonoBehaviour, IPointerClickHandler
    {
        public SpriteRenderer spriteRenderer;
        public Tile TileData;
        private BoardController _board;

        public void Init(Tile tile, Sprite sprite, BoardController board)
        {
            TileData = tile;
            spriteRenderer.sprite = sprite;
            _board = board;
        }

        public void SetPosition(Vector2Int gridPos, bool instant = false)
        {
            var targetPos = new Vector3(gridPos.x, gridPos.y, 0f);
            if (instant)
            {
                transform.localPosition = targetPos;
            }
            else
            {
                StartCoroutine(MoveTo(targetPos));
            }
        }

        private IEnumerator MoveTo(Vector3 target)
        {
            float t = 0;
            Vector3 start = transform.localPosition;
            while (t < 1f)
            {
                t += Time.deltaTime * 5f;
                transform.localPosition = Vector3.Lerp(start, target, t);
                yield return null;
            }

            transform.localPosition = target;
        }

        public IEnumerator PlayDestroyAnimation()
        {
            float t = 0;
            Vector3 startScale = transform.localScale;

            while (t < 1f)
            {
                t += Time.deltaTime * 3f;
                transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);
                yield return null;
            }

            Destroy(gameObject);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _board.OnTileClicked(this);
        }

        public void Highlight(bool on)
        {
            spriteRenderer.color = on ? Color.yellow : Color.white;
        }
    }
}