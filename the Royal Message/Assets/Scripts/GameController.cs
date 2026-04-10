using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class GameController : MonoBehaviour
{
    Camera mainCamera;
    GameController CharacterMovement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void LateUpdate()
    {
        
    }
}
