using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoSingleton<DeckManager>
{
    public List<CardData> currentDeck = new List<CardData>();

    public void LoadDeck(List<CardData> savedDeck)
    {
        currentDeck = new List<CardData>(savedDeck);
        Debug.Log("Deck Loaded");
    }

    public void AddCard(CardData card)
    {
        if (!currentDeck.Contains(card))
        {
            currentDeck.Add(card);
            Debug.Log($"Card added: {card.cardName}");
        }
    }

    public void RemoveCard(CardData card)
    {
        if (currentDeck.Contains(card))
        {
            currentDeck.Remove(card);
            Debug.Log($"Card removed: {card.cardName}");
        }
    }

    public List<CardData> GetCurrentDeck()
    {
        return currentDeck;
    }

    public void ClearDeck()
    {
        currentDeck.Clear();
        Debug.Log("Deck cleared");
    }
}