using UnityEngine;
using UnityEngine.UI;

public class UpdateSwapButtonUI : MonoBehaviour
{
    [SerializeField]
    private SelectionMenuType buttonType;

    [SerializeField]
    private GameplayController gameplayController;

    private void Start()
    {
        switch (buttonType)
        {
            case SelectionMenuType.Cars:
                GetComponent<Image>().sprite = gameplayController.availableCars[GameManager.Instance.selectedCar].GetComponent<SpriteRenderer>().sprite;
                break;
            case SelectionMenuType.DirtCars:
                GetComponent<Image>().sprite = gameplayController.availableDirtCars[GameManager.Instance.selectedDirtCar].GetComponent<SpriteRenderer>().sprite;
                break;
            case SelectionMenuType.Boats:
                GetComponent<Image>().sprite = gameplayController.availableBoats[GameManager.Instance.selectedBoat].GetComponent<SpriteRenderer>().sprite;
                break;
            default:
                break;
        }

    }
}
