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
                vc.Go();
                vc.ReturnToDefaultVelocity();
                return;
            }

            if (collision.gameObject.CompareTag("Car"))
            {
                vc.Stop();
                return;
            }

            if (collision.gameObject.CompareTag("DirtCar"))
            {
                vc.Stop();
                return;
            }
        }
      
        if(isRoad)
        {
            if (collision.gameObject.CompareTag("Boat"))
            {
                vc.Stop();
                return;
            }

            if (collision.gameObject.CompareTag("Car"))
            {
                vc.Go();
                vc.ReturnToDefaultVelocity();
                return;
            }

            if (collision.gameObject.CompareTag("DirtCar"))
            {
                vc.Go();
                vc.SetMaxSpeed(offRoadSpeed);
                return;
            }
        }

        if (isDirt)
        {
            if (collision.gameObject.CompareTag("Boat"))
            {
                vc.Stop();
                return;
            }

            if (collision.gameObject.CompareTag("Car"))
            {
                vc.Go();
                vc.SetMaxSpeed(offRoadSpeed);
                return;
            }

            if (collision.gameObject.CompareTag("DirtCar"))
            {
                vc.Go();
                vc.ReturnToDefaultVelocity();
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            collision.gameObject.GetComponent<VehicleController>().Stop();
        }
        else 
        {
            collision.gameObject.GetComponent<VehicleController>().SetMaxSpeed(offRoadSpeed);
        }

    }
}
