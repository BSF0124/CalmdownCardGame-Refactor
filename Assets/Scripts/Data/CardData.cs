using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class CardData : ScriptableObject
{
    public int id;
    public string cardName;
    public Enums.Rarity rarity;
    public Enums.Type type;
    public int power;
    public Sprite cardSprite;
}
