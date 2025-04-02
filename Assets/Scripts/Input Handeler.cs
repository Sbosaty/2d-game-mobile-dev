using UnityEngine;

public class InputHandeler : MonoBehaviour
{

    public static int GetTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            float screenMidPoint = Screen.width / 2;

            if (touch.phase == TouchPhase.Began)
            {
                return touch.position.x >= screenMidPoint ? 1 : -1;
            }
        }
        return 0;
    }
}
