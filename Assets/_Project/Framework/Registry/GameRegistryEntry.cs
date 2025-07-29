using System;
using Eflatun.SceneReference;
using UnityEngine;

namespace _Project.Framework
{
    public class GameRegistryEntry : ScriptableObject, IMiniGameInfo
    {
        [SerializeField] private string gameId;
        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite previewImage;
        [SerializeField] private SceneReference sceneReference;
        [SerializeField] private DifficultyLevel difficulty = DifficultyLevel.Normal;
        [SerializeField] private GameCategory category;

        public string GameId => gameId;
        public string DisplayName => displayName;
        public string Description => description;
        public Sprite Icon => icon;
        public Sprite PreviewImage => previewImage;
        public SceneReference SceneReference => sceneReference;
        public DifficultyLevel Difficulty => difficulty;
        public GameCategory Category => category;

        public GameRegistryEntry(
            string gameId,
            string displayName,
            string description,
            SceneReference sceneReference,
            GameCategory category = GameCategory.Other)
        {
            this.gameId = gameId;
            this.displayName = displayName;
            this.description = description;
            this.sceneReference = sceneReference;
            this.category = category;
        }
    }
}