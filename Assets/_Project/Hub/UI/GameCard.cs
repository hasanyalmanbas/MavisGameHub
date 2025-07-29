using _Project.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Hub.UI
{
    public class GameCard : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button button;
        
        public void Init(IMiniGameInfo miniGameInfo, UnityAction onClick)
        {
            image.sprite = miniGameInfo.PreviewImage;
            title.text = miniGameInfo.DisplayName;
            description.text = miniGameInfo.Description;
            button.onClick.AddListener(onClick);
        }
    }
}