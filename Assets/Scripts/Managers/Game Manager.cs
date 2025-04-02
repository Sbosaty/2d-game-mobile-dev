using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score = 0;

    private List<int> unlockedLevels = new List<int>();

    private List<int> unlockedCars = new List<int>();
    private List<int> unlockedDirtCars = new List<int>();
    private List<int> unlockedBoats = new List<int>();

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

        SetSelectedCar(0);
        SetSelectedDirtCar(0);
        SetSelectedBoat(0);

        SaveGameData();
    }


    public void UnlockLevel(int levelIndex)
    {
        if (!unlockedLevels.Contains(levelIndex))
        {
            unlockedLevels.Add(levelIndex);
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

    public bool IsLevelUnlocked(int levelIndex)
    {
        return unlockedLevels.Contains(levelIndex);
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

    private void SaveGameData()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetString("UnlockedLevels", string.Join(",", unlockedLevels));
        PlayerPrefs.SetString("UnlockedCars", string.Join(",", unlockedCars));
        PlayerPrefs.SetString("UnlockedDirtCars", string.Join(",", unlockedDirtCars));
        PlayerPrefs.SetString("UnlockedBoats", string.Join(",", unlockedBoats));

        PlayerPrefs.SetInt("SelectedCar", selectedCar);
        PlayerPrefs.SetInt("SelectedDirtCar", selectedDirtCar);
        PlayerPrefs.SetInt("SelectedBoat", selectedBoat);

        PlayerPrefs.Save();
    }

    private void LoadGameData()
    {
        score = PlayerPrefs.GetInt("Score", 0);

        // Load unlocked levels
        string unlockedLevelsString = PlayerPrefs.GetString("UnlockedLevels", "");
        if (!string.IsNullOrEmpty(unlockedLevelsString))
            unlockedLevels = new List<int>(Array.ConvertAll(unlockedLevelsString.Split(','), int.Parse));

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
        selectedCar = PlayerPrefs.GetInt("SelectedCar", -1);
        selectedDirtCar = PlayerPrefs.GetInt("SelectedDirtCar", -1);
        selectedBoat = PlayerPrefs.GetInt("SelectedBoat", -1);
    }
}
