using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public int currentStage = 0;
    // public Deck playerDeck;

    public void StartGame()
    {
        Debug.Log($"Game Start - Stage: {currentStage}");
    }

    public void SetStage(int stage)
    {
        currentStage = stage;
    }

    // public void SetPlayerDeck(Deck deck)
    // {
    //     playerDeck = deck;
    // }

    public void EndGame()
    {
        Debug.Log("Game Over");
    }
}
