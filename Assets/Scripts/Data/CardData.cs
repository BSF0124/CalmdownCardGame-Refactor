using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]

public class CardData : ScriptableObject
{
    public int id;
    public string cardName;
    public CardRarity cardRarity;
    public CardType cardType;
    public int attackPower;
    public Sprite cardSprite;
}

public enum CardRarity
{
    Null,
    N,
    R,
    SR,
    SSR
}

public enum CardType
{
    Null,
    Rock,
    Scissors,
    Paper,
    All
}