using UnityEngine;

public class Enums : MonoBehaviour
{
    public enum Rarity
    {
        Null,
        N,
        R,
        SR,
        SSR,
    }

    public enum Type
    {
        Null,
        Rock,
        Scissors,
        Paper,
        All,
    }

    public enum SceneType
    {
        Null = 0,
        MainMenu,
        Stage,
        Dual,
        CutScene,
    }

    public enum StageState
    {
        Locked = 0,
        Unlocked,
        Cleared
    }
}
