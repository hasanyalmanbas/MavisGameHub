using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Framework.Editor
{
    public class GamesWindow : EditorWindow
    {
        private string gameName = "NewGame";

        [MenuItem("Tools/Mavis Game Hub/Games Window")]
        public static void ShowWindow() =>
            GetWindow<GamesWindow>("Games Window");

        private void OnGUI()
        {
            GUILayout.Label("Create New Game", EditorStyles.boldLabel);
            gameName = EditorGUILayout.TextField("Game Name", gameName);

            if (GUILayout.Button("Create"))
            {
                CreateGame(gameName);
                AssetDatabase.Refresh();
            }
        }

        private static void CreateGame(string gameName)
        {
            var rootPath = Path.Combine("Assets", "_Project", "Games", gameName);
            var scriptsPath = Path.Combine(rootPath, "Scripts");
            var resourcesPath = Path.Combine(rootPath, "Resources");

            string[] paths =
            {
                "Scenes",
                "Scripts/Domain",
                "Scripts/Domain/Entities",
                "Scripts/Domain/Services",
                "Scripts/Domain/Ports",
                "Scripts/Application",
                "Scripts/Application/UseCases",
                "Scripts/Infrastructure",
                "Scripts/Infrastructure/Config",
                "Scripts/Infrastructure/Services",
                "Scripts/Presentation",
                "Scripts/Presentation/Views",
                "Scripts/Presentation/Controllers",
                "Scripts/Presentation/ViewModels",
                "Scripts/Editor",
                "Prefabs",
                "Resources",
                "Textures",
                "Sounds",
                "Models",
            };

            Utils.CreateFolderAndSubfolders(rootPath, paths);

            var createdGameRegistry = CreateInstance<GameRegistryEntry>();
            AssetDatabase.CreateAsset(createdGameRegistry, resourcesPath + "/" + gameName + ".asset");
            Resources.Load<GameRegistryAsset>("GameRegistryAsset").AddGame(createdGameRegistry);
            AssetDatabase.SaveAssets();
            
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = createdGameRegistry;

            /*
            var asmdefRuntime =
                $@"{{
                  ""name"": ""Mavis.Games.{gameName}"",
                  ""references"": [""Mavis.Framework.Runtime""]
                }}";

            File.WriteAllText(Path.Combine(scriptsRoot, $"Mavis.Games.{gameName}.asmdef"), asmdefRuntime);
            */

            Debug.Log($"{gameName} game created. Added folder: Games/{gameName}");
        }
    }
}