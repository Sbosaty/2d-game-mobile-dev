using UnityEngine;

public class GroundTrigger : MonoBehaviour
{
    private float offRoadSpeed = 2;

    [SerializeField]
    private bool isWater = false, isRoad = false, isDirt = false;
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var vc = collision.GetComponent<VehicleController>();

        if (isWater)
        {
            if (collision.gameObject.CompareTag("Boat"))
            {
                vc.ReturnToDefaultVelocity();
                return;
            }

            if (collision.gameObject.CompareTag("Car"))
            {
                vc.SetMaxSpeed(1);
                return;
            }

            if (collision.gameObject.CompareTag("DirtCar"))
            {
                vc.SetMaxSpeed(1);
                return;
            }
        }
      
        if(isRoad)
        {
            if (collision.gameObject.CompareTag("Boat"))
            {
                vc.SetMaxSpeed(offRoadSpeed);
                return;
            }

            if (collision.gameObject.CompareTag("Car"))
            {
                vc.ReturnToDefaultVelocity();
                return;
            }

            if (collision.gameObject.CompareTag("DirtCar"))
            {
                vc.SetMaxSpeed(offRoadSpeed);
                return;
            }
        }

        if (isDirt)
        {
            if (collision.gameObject.CompareTag("Boat"))
            {
                vc.SetMaxSpeed(offRoadSpeed);
                return;
            }

            if (collision.gameObject.CompareTag("Car"))
            {
                vc.SetMaxSpeed(offRoadSpeed);
                return;
            }

            if (collision.gameObject.CompareTag("DirtCar"))
            {
                vc.ReturnToDefaultVelocity();
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        collision.gameObject.GetComponent<VehicleController>().SetMaxSpeed(offRoadSpeed);
    }
}
