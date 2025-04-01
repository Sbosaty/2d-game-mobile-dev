using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score = 0; // Example variable to store game data

    public GameObject selectedCar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicate GameManagers
        }
    }

    // Example method
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}
