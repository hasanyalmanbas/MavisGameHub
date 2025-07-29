using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Framework
{
    [CreateAssetMenu(fileName = "GameRegistryAsset", menuName = "MavisGameHub/Game/Game Registry Asset")]
    public class GameRegistryAsset : ScriptableObject
    {
        [Header("Registered Games")] [SerializeField]
        private List<GameRegistryEntry> games = new();

        public IReadOnlyList<GameRegistryEntry> Games => games;

        public void PopulateRegistry(IGameRegistry registry)
        {
            foreach (var game in games.Where(game => game != null))
            {
                registry.RegisterGame(game);
            }

            Debug.Log($"[GameRegistryAsset] Populated registry with {games.Count} games");
        }

#if UNITY_EDITOR
        public void AddGame(GameRegistryEntry entry)
        {
            if (entry != null && !games.Contains(entry))
            {
                games.Add(entry);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }

        public void RemoveGame(GameRegistryEntry entry)
        {
            if (games.Remove(entry))
            {
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}