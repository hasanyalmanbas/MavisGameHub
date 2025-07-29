using System;
using _Project.Core;
using _Project.Games.Match3.Scripts.Domain.Events;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace _Project.Games.Match3.Scripts.Presentation.Views
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [Inject] private readonly IEventBus _eventBus;

        private void OnEnable()
        {
            _eventBus.Subscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedEvent evt)
        {
            scoreText.text = $"Score: {evt.NewScore}";
        }
    }
}