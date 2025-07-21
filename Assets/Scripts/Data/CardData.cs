using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card", order = 0)]
public class CardData : ScriptableObject
{
    public int cardID;
    public string cardName;
    public Enums.Rarity rarity;
    public Enums.Type type;
    public int power;
    public Sprite cardSprite;
}