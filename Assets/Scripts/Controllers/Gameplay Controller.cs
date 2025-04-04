using System.Collections;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance { get; private set; }

    [Header("Available Vehicles")]
    public GameObject[] availableCars;      // All car prefabs
    public GameObject[] availableDirtCars;  // All dirt car prefabs
    public GameObject[] availableBoats;     // All boat prefabs

    public Transform spawnPoint;

    public float levelTime = 150;

    public CameraFollow camController;

    private GameObject spawnedCar;
    private GameObject spawnedDirtCar;
    private GameObject spawnedBoat;

    private Transform newTransform;

    private Coroutine scoreCoroutine;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1.0f;

        GameManager.Instance.score = levelTime;

        Application.targetFrameRate = (60);

        GameObject empty = new GameObject();

        newTransform = empty.transform;

        Destroy(empty);

        SpawnSelectedVehicles();

        camController.FindTarget();

        EventsHandeler.GameStart();
    }

    private void Update()
    {
        EventsHandeler.UpdateScore();
    }

    private void OnEnable()
    {
        EventsHandeler.OnGameStart += StartScoreDecreasing;
        EventsHandeler.OnGameWin += AddReward;

    }

    private void OnDisable()
    {
        EventsHandeler.OnGameStart -= StartScoreDecreasing;
        EventsHandeler.OnGameWin -= AddReward;
    }

    private void AddReward() 
    {
        GameManager.Instance.coins += 3 * (int)GameManager.Instance.score;
        GameManager.Instance.SaveGameData();
    }

    private void StartScoreDecreasing()
    {
        Debug.Log("Starting ....");

        if (scoreCoroutine == null)
            scoreCoroutine = StartCoroutine(decreaseTimer());
    }


    private IEnumerator decreaseTimer()
    {
        while (GameManager.Instance.score > 0)
        {
            GameManager.Instance.score -= 5 * Time.deltaTime;
            GameManager.Instance.score = Mathf.Max(GameManager.Instance.score, 0);
            yield return null;
        }

        Time.timeScale = 0;
        EventsHandeler.GameOver();
    }

    public void SwapVehicle(string type)
    {

        if (spawnedCar.activeSelf)
        {
            newTransform = spawnedCar.transform;
        }
        if (spawnedDirtCar.activeSelf)
        {
            newTransform = spawnedDirtCar.transform;
        }
        if (spawnedBoat.activeSelf)
        {
            newTransform = spawnedBoat.transform;
        }

        spawnedCar.SetActive(false);

        spawnedDirtCar.SetActive(false);

        spawnedBoat.SetActive(false);

        switch (type)
        {
            case "Car":
                spawnedCar.SetActive(true);
                camController.FindTarget();
                spawnedCar.transform.position = newTransform.position;
                spawnedCar.transform.rotation = newTransform.rotation;
                spawnedCar.GetComponent<Rigidbody2D>().linearVelocity = newTransform.gameObject.GetComponent<Rigidbody2D>().linearVelocity;
                break;
            case "DirtCar":
                spawnedDirtCar.SetActive(true);
                camController.FindTarget();
                spawnedDirtCar.transform.position = newTransform.position;
                spawnedDirtCar.transform.rotation = newTransform.rotation;
                spawnedDirtCar.GetComponent<Rigidbody2D>().linearVelocity = newTransform.gameObject.GetComponent<Rigidbody2D>().linearVelocity;

                break;
            case "Boat":
                spawnedBoat.SetActive(true);
                camController.FindTarget();
                spawnedBoat.transform.position = newTransform.position;
                spawnedBoat.transform.rotation = newTransform.rotation;
                spawnedBoat.GetComponent<Rigidbody2D>().linearVelocity = newTransform.gameObject.GetComponent<Rigidbody2D>().linearVelocity;

                break;
            default:
                break;
        }
    }

    public void SpawnSelectedVehicles()
    {
        // Get selected vehicle indexes from GameManager
        int selectedCarIndex = GameManager.Instance.selectedCar;
        int selectedDirtCarIndex = GameManager.Instance.selectedDirtCar;
        int selectedBoatIndex = GameManager.Instance.selectedBoat;

        // Spawn Car
        if (selectedCarIndex >= 0 && selectedCarIndex < availableCars.Length)
        {
            spawnedCar = Instantiate(availableCars[selectedCarIndex], spawnPoint.position, spawnPoint.rotation);
        }

        // Spawn Dirt Car
        if (selectedDirtCarIndex >= 0 && selectedDirtCarIndex < availableDirtCars.Length)
        {
            spawnedDirtCar = Instantiate(availableDirtCars[selectedDirtCarIndex], spawnPoint.position, spawnPoint.rotation);
            spawnedDirtCar.SetActive(false);
        }

        // Spawn Boat
        if (selectedBoatIndex >= 0 && selectedBoatIndex < availableBoats.Length)
        {
            spawnedBoat = Instantiate(availableBoats[selectedBoatIndex], spawnPoint.position, spawnPoint.rotation);
            spawnedBoat.SetActive(false);

        }
    }
}
