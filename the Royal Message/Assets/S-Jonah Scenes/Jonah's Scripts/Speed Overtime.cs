using UnityEditor.SceneManagement;
using UnityEngine;

public class SpeedOvertime : MonoBehaviour
{
    // public varaibles that help with speed
    private float additionalspeed;
    public CharacterMovement CharacterMovementScript;
    public SpriteRenderer characterSR;
    private SpriteRenderer rb;
    public float time = 0.00f;
    public float additonalspeed = +2.50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // getting the componets of the characters movement
        characterSR = GetComponent<SpriteRenderer>();
        rb = GetComponent<SpriteRenderer>();
        CharacterMovementScript = characterSR.GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // the time scale
        time += Time.deltaTime;

        
        // happens if it has reached a current amount of time
        if(time > 30.0f)
        {
            CharacterMovementScript.speed = Time.deltaTime * 2 * additionalspeed;
            Debug.Log("you been granted even more speed");
        }
            if(time >= 18.0f)
            {
                CharacterMovementScript.speed = Time.deltaTime * additionalspeed;
                Debug.Log("you been granted more speed");
            }
        if (time < 9.0f)
        {
            Debug.Log("no speed gained yet");
        }
    }
}
