using System;
using UnityEngine;

public class EventsHandeler : MonoBehaviour
{
    public static event Action OnGameStart;

    public static event Action OnGameOver;

    public static event Action OnGameWin;

    public static event Action OnGamePause;

    public static event Action OnGameResume;

    public static event Action<int> OnScoreChanged;

    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public static void GameWin()
    {
        OnGameWin?.Invoke();
    }

    public static void GamePause() { OnGamePause?.Invoke(); }

    public static void GameResume() { OnGameResume?.Invoke(); }

    public static void ScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
    }
}
