using Eflatun.SceneReference;
using UnityEngine;

namespace _Project.Framework
{
    public interface IMiniGameInfo
    {
        string GameId { get; }
        
        string DisplayName { get; }

        string Description { get; }

        Sprite Icon { get; }

        Sprite PreviewImage { get; }

        SceneReference SceneReference { get; }

        GameCategory Category { get; }
    }
}