using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionMenuType { Cars, DirtCars, Boats};

public class CarSelectorController : MonoBehaviour
{
    [SerializeField]
    private SelectionMenuType menuType;

    public List<Button> carButtons; 
    public List<Button> buyButtons; 
    public List<Text> buttonTexts;
    public List<int> vehiclePrices;

    private Color lockedColor = new Color(0, 0, 0, 0.8f); 
    private Color activatedColor = new Color(1, 1, 1, 120f / 255f); 
    private Color selectedColor = Color.white; 

    List<int> unlockedVehicles = new List<int>();

    private int selectedVehicle;


    void Start()
    {
        UpdateCarButtons();
    }

    public void UpdateCarButtons()
    {
        switch (menuType)
        {
            case SelectionMenuType.Cars:
                unlockedVehicles = GameManager.Instance.unlockedCars;
                break;
            case SelectionMenuType.DirtCars:
                unlockedVehicles = GameManager.Instance.unlockedDirtCars;

                break;
            case SelectionMenuType.Boats:
                unlockedVehicles = GameManager.Instance.unlockedBoats;

                break;
            default:
                break;
        }

        switch (menuType)
        {
            case SelectionMenuType.Cars:
                selectedVehicle = GameManager.Instance.selectedCar;
                break;
            case SelectionMenuType.DirtCars:
                selectedVehicle = GameManager.Instance.selectedDirtCar;

                break;
            case SelectionMenuType.Boats:
                selectedVehicle = GameManager.Instance.selectedBoat;

                break;
            default:
                break;
        }

        for (int i = 0; i < carButtons.Count; i++)
        {
            Image buttonImage = carButtons[i].GetComponent<Image>();

            if (unlockedVehicles.Contains(i))
            {
                buttonTexts[i].gameObject.SetActive(true);

                buttonTexts[i].text = carButtons[i].name;

                    buyButtons[i].gameObject.SetActive(false);

                if (i == selectedVehicle)
                {
                    buttonImage.color = selectedColor;
                }
                else
                {
                    buttonImage.color = activatedColor;
                }
            }
            else
            {
                buttonTexts[i].text = "Locked";

                buttonImage.color = lockedColor;

                buttonTexts[i].gameObject.SetActive(false);


                    buyButtons[i].gameObject.SetActive(true);
                    buyButtons[i].GetComponentInChildren<Text>().text = "$ " + vehiclePrices[i];
                
            }

            int index = i; 

            carButtons[i].onClick.AddListener(() => SelectCar(index));

                buyButtons[i].onClick.AddListener(() => BuyCar(index));


        }
    }

    public void SelectCar(int index)
    {

        if (unlockedVehicles.Contains(index))
        {
            switch (menuType)
            {
                case SelectionMenuType.Cars:
                    GameManager.Instance.selectedCar = index;

                    break;
                case SelectionMenuType.DirtCars:
                    GameManager.Instance.selectedDirtCar = index;

                    break;
                case SelectionMenuType.Boats:
                    GameManager.Instance.selectedBoat = index;

                    break;
                default:
                    break;
            }
            
            UpdateCarButtons();
            
            GameManager.Instance.SaveGameData();
        }
    }

    public void BuyCar(int index)
    {
        int carPrice = vehiclePrices[index];

        if (GameManager.Instance.coins >= carPrice)
        {
            GameManager.Instance.coins -= carPrice; // Deduct coins
            switch (menuType)
            {
                case SelectionMenuType.Cars:
                    GameManager.Instance.UnlockCar(index);
                    break;
                case SelectionMenuType.DirtCars:
                    GameManager.Instance.UnlockDirtCar(index);

                    break;
                case SelectionMenuType.Boats:
                    GameManager.Instance.UnlockBoat(index);

                    break;
                default:
                    break;
            }
            UpdateCarButtons(); // Refresh UI
            buyButtons[index].gameObject.SetActive(false);
            buttonTexts[index].gameObject.SetActive(true);
            GameManager.Instance.SaveGameData();
        }
    }

}
