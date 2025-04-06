using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
 

    public float score = 0.0f;
    public int coins = 300;


    private List<string> unlockedLevels = new List<string>();

    public List<int> unlockedCars = new List<int>();
    public List<int> unlockedDirtCars = new List<int>();
    public List<int> unlockedBoats = new List<int>();

    public int selectedCar = -1;
    public int selectedDirtCar = -1;
    public int selectedBoat = -1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameData();
        }
        else
        {
            Destroy(gameObject);
        }

        UnlockCar(0);
        UnlockDirtCar(0);
        UnlockBoat(0);
        UnlockLevel("Level 1");


        SetSelectedCar(0);
        SetSelectedDirtCar(0);
        SetSelectedBoat(0);
    }


    public void UnlockLevel(string levelID)
    {
        if (!unlockedLevels.Contains(levelID))
        {
            unlockedLevels.Add(levelID);
            SaveGameData();
        }
    }

    public void UnlockCar(int carIndex)
    {
        if (!unlockedCars.Contains(carIndex))
        {
            unlockedCars.Add(carIndex);
            SaveGameData();
        }
    }

    public void UnlockDirtCar(int dirtCarIndex)
    {
        if (!unlockedDirtCars.Contains(dirtCarIndex))
        {
            unlockedDirtCars.Add(dirtCarIndex);
            SaveGameData();
        }
    }

    public void UnlockBoat(int boatIndex)
    {
        if (!unlockedBoats.Contains(boatIndex))
        {
            unlockedBoats.Add(boatIndex);
            SaveGameData();
        }
    }

    public void SetSelectedCar(int carIndex)
    {
        selectedCar = carIndex;
        SaveGameData();
    }

    public void SetSelectedDirtCar(int dirtCarIndex)
    {
        selectedDirtCar = dirtCarIndex;
        SaveGameData();
    }

    public void SetSelectedBoat(int boatIndex)
    {
        selectedBoat = boatIndex;
        SaveGameData();
    }

    public bool IsLevelUnlocked(string levelID)
    {
        return unlockedLevels.Contains(levelID);
    }

    public bool IsCarUnlocked(int carIndex)
    {
        return unlockedCars.Contains(carIndex);
    }

    public bool IsDirtCarUnlocked(int dirtCarIndex)
    {
        return unlockedDirtCars.Contains(dirtCarIndex);
    }

    public bool IsBoatUnlocked(int boatIndex)
    {
        return unlockedBoats.Contains(boatIndex);
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetFloat ("Score", score);
        PlayerPrefs.SetInt ("Coins", coins);
        PlayerPrefs.SetString("UnlockedLevels", string.Join(",", unlockedLevels));
        PlayerPrefs.SetString("UnlockedCars", string.Join(",", unlockedCars));
        PlayerPrefs.SetString("UnlockedDirtCars", string.Join(",", unlockedDirtCars));
        PlayerPrefs.SetString("UnlockedBoats", string.Join(",", unlockedBoats));

        PlayerPrefs.SetInt("SelectedCar", selectedCar);
        PlayerPrefs.SetInt("SelectedDirtCar", selectedDirtCar);
        PlayerPrefs.SetInt("SelectedBoat", selectedBoat);

        PlayerPrefs.Save();
    }

    public void LoadGameData()
    {
        score = PlayerPrefs.GetInt("Score", 0);
        coins = PlayerPrefs.GetInt("Coins", 300);

        // Load unlocked levels
        string unlockedLevelsString = PlayerPrefs.GetString("UnlockedLevels", "");
        if (!string.IsNullOrEmpty(unlockedLevelsString))
            unlockedLevels = new List<string>(unlockedLevelsString.Split(','));

        // Load unlocked cars
        string unlockedCarsString = PlayerPrefs.GetString("UnlockedCars", "");
        if (!string.IsNullOrEmpty(unlockedCarsString))
            unlockedCars = new List<int>(Array.ConvertAll(unlockedCarsString.Split(','), int.Parse));

        // Load unlocked dirt cars
        string unlockedDirtCarsString = PlayerPrefs.GetString("UnlockedDirtCars", "");
        if (!string.IsNullOrEmpty(unlockedDirtCarsString))
            unlockedDirtCars = new List<int>(Array.ConvertAll(unlockedDirtCarsString.Split(','), int.Parse));

        // Load unlocked boats
        string unlockedBoatsString = PlayerPrefs.GetString("UnlockedBoats", "");
        if (!string.IsNullOrEmpty(unlockedBoatsString))
            unlockedBoats = new List<int>(Array.ConvertAll(unlockedBoatsString.Split(','), int.Parse));

        // Load selected vehicles
        selectedCar = PlayerPrefs.GetInt("SelectedCar", 0);
        selectedDirtCar = PlayerPrefs.GetInt("SelectedDirtCar", 0);
        selectedBoat = PlayerPrefs.GetInt("SelectedBoat", 0);
    }
}
