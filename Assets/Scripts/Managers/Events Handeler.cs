using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EventsHandeler : MonoBehaviour
{
    public static event Action OnGameOver;

    public static event Action OnGameStart;

    public static event Action OnGameWin;

    public static event Action OnGamePause;

    public static event Action OnGameResume;

    public static event Action OnUpdateScore;

    public static event Action OnUpdateCoins;

    public static void GameOver()
    {
        OnGameOver?.Invoke();
    }
    
    public static void UpdateCoins()
    {
        OnUpdateCoins?.Invoke();
    }

    public static void GameWin()
    {
        OnGameWin?.Invoke();
    }  
    
    public static void GameStart()
    {
        OnGameStart?.Invoke();
    }

    public static void UpdateScore() 
    {
        OnUpdateScore?.Invoke();

    }

    public static void GamePause() 
    {
        OnGamePause?.Invoke();
    }

    public static void GameResume()
    { 
        OnGameResume?.Invoke(); 
    }
}
