using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

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

    public bool IsLevelUnlocked(string levelID) => unlockedLevels.Contains(levelID);
    public bool IsCarUnlocked(int carIndex) => unlockedCars.Contains(carIndex);
    public bool IsDirtCarUnlocked(int dirtCarIndex) => unlockedDirtCars.Contains(dirtCarIndex);
    public bool IsBoatUnlocked(int boatIndex) => unlockedBoats.Contains(boatIndex);

    public void SaveGameData()
    {
        GameData data = new GameData
        {
            coins = coins,
            unlockedLevels = unlockedLevels,
            unlockedCars = unlockedCars,
            unlockedDirtCars = unlockedDirtCars,
            unlockedBoats = unlockedBoats,
            selectedCar = selectedCar,
            selectedDirtCar = selectedDirtCar,
            selectedBoat = selectedBoat
        };

        string json = JsonUtility.ToJson(data);
        StartCoroutine(SendDataToServer(json));
    }

    public void LoadGameData()
    {
        StartCoroutine(GetDataFromServer());
    }

    IEnumerator SendDataToServer(string json)
    {
        string userId = SystemInfo.deviceUniqueIdentifier;
        string url = $"http://localhost:3000/save/{userId}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new UTF8Encoding().GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
            Debug.Log("Game data saved successfully!");
        else
            Debug.LogError("Error saving game data: " + request.error);
    }

    IEnumerator GetDataFromServer()
    {
        string userId = SystemInfo.deviceUniqueIdentifier;
        string url = $"http://localhost:3000/load/{userId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;

            try
            {
                GameData data = JsonUtility.FromJson<GameData>(json);

                if (data != null)
                {
                    coins = data.coins;
                    unlockedLevels = data.unlockedLevels ?? new List<string>();
                    unlockedCars = data.unlockedCars ?? new List<int>();
                    unlockedDirtCars = data.unlockedDirtCars ?? new List<int>();
                    unlockedBoats = data.unlockedBoats ?? new List<int>();
                    selectedCar = data.selectedCar;
                    selectedDirtCar = data.selectedDirtCar;
                    selectedBoat = data.selectedBoat;

                    Debug.Log("Game data loaded successfully!");
                }
                else
                {
                    Debug.LogWarning("Received empty or invalid game data. Using defaults.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to parse game data: {e.Message}");
            }
        }
        else
        {
            Debug.LogWarning($"Failed to load game data: {request.error}");
        }
    }
}

[Serializable]
public class GameData
{
    public int coins;
    public List<string> unlockedLevels;
    public List<int> unlockedCars;
    public List<int> unlockedDirtCars;
    public List<int> unlockedBoats;
    public int selectedCar;
    public int selectedDirtCar;
    public int selectedBoat;
}
