using _Project.Core;
using _Project.Framework;
using Reflex.Attributes;
using UnityEngine;

namespace _Project.Hub.UI
{
    public class HubView : MonoBehaviour
    {
        [Inject] private readonly IGameRegistry _gameRegistry;
        [Inject] private readonly ISceneManager _sceneManager;

        [SerializeField] private Transform scrollViewContent;
        [SerializeField] private GameCard gameCardPrefab;

        private void Start()
        {
            foreach (var gameRegistryRegisteredGame in _gameRegistry.RegisteredGames)
            {
                var gameCard = Instantiate(gameCardPrefab, scrollViewContent);
                gameCard.Init(gameRegistryRegisteredGame, () =>
                {
                    var sceneKey = gameRegistryRegisteredGame.SceneReference.Address;
                    _sceneManager.LoadSceneWithLoader(sceneKey);
                });
            }
        }
    }
}