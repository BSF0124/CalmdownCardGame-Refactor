using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardEntry : MonoBehaviour
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private Text countText;

        public void Init(Sprite sprite, int count)
        {
            cardImage.sprite = sprite;
            countText.text = $"x{count}";
        }
    }
}