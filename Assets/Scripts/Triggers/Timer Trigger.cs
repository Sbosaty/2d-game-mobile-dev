using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    [SerializeField]
    private float timerAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car") || collision.CompareTag("DirtCar") || collision.CompareTag("Boat")) 
        {
            GameManager.Instance.score += timerAmount;
            Destroy(gameObject);
        }
    }
}
