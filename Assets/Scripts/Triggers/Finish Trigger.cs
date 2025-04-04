using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Car")|| collision.CompareTag("DirtCar") || collision.CompareTag("Boat"))
            EventsHandeler.GameWin();
    }
}
