using UnityEngine;

public class destroyeverythingoutsidecamera : MonoBehaviour
{
   
    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        // Convert object position to screen points (0-1)
        Vector3 screenPoint = mainCam.WorldToViewportPoint(transform.position);

        // Check if outside [0,1] range
        if (screenPoint.x < 0 || screenPoint.x > 1 ||
            screenPoint.y < 0 || screenPoint.y > 1 ||
            screenPoint.z < 0) // z < 0 means behind camera
        {
            Destroy(gameObject);
        }
    }
}

