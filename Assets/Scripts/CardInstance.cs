using System.Collections.Generic;
using UnityEngine;

public class CardInstance
{
    public CardData Data { get; }
    public int Id => Data.id;
    public string Name => Data.cardName;
    public CardRarity Rarity => Data.cardRarity;
    public int AttackPower => Data.attackPower;

    public int MaxHP { get; }
    public int CurrentHP { get; private set; }

    public List<CardType> SSRRemainingTypes { get; private set; }

    public bool IsSSR => Rarity == CardRarity.SSR;

    public bool IsDead => CurrentHP <= 0;

    public CardType CurrentType
    {
        get
        {
            if (IsSSR)
                return SSRRemainingTypes.Count > 0
                    ? SSRRemainingTypes[0]
                    : Data.cardType;
            return Data.cardType;
        }
    }

    public CardInstance(CardData data)
    {
        Data = data;
        MaxHP = data.cardRarity switch
        {
            CardRarity.N => 1,
            CardRarity.R => 2,
            CardRarity.SR => 3,
            CardRarity.SSR => 1,
            _ => 1
        };
        CurrentHP = MaxHP;

        if (IsSSR)
            SSRRemainingTypes = new List<CardType> { CardType.Rock, CardType.Scissors, CardType.Paper };
    }

    public void TakeDamage(int amount)
    {
        CurrentHP = Mathf.Max(0, CurrentHP - amount);
    }

    public void ConsumeSSRType(CardType opponentType)
    {
        if (!IsSSR || SSRRemainingTypes == null) return;

        CardType toRemove = opponentType switch
        {
            CardType.Rock => CardType.Paper,
            CardType.Paper => CardType.Scissors,
            CardType.Scissors => CardType.Rock,
            _ => CardType.Null
        };
        SSRRemainingTypes.Remove(toRemove);
    }

    public override string ToString()
        => $"{Name} (ID:{Id}, {Rarity}, HP:{CurrentHP}/{MaxHP}, Type:{CurrentType})";
}
