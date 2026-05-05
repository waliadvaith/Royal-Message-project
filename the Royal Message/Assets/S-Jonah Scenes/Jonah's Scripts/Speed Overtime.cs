using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedOvertime : MonoBehaviour
{
    // public varaibles that help with speed
    private float additionalspeed;
    public CharacterMovement CharacterMovementScript;
    public SpriteRenderer characterSR;
    private SpriteRenderer rb;
    public float time = 0.00f;
    public float increase;

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
        if(time > 20.0f)
        {
            increase = 3.0f * Time.deltaTime;
            CharacterMovementScript.speed = increase * Time.deltaTime;
            Debug.Log("you been granted even more speed");
        }
            if(time >= 10.0f)
            {
                    if(time < 20.0f)
                    {
                        increase = 2.0f * Time.deltaTime;
                        CharacterMovementScript.speed = increase * Time.deltaTime;
                        Debug.Log("you been granted more speed");
                    }
                
            }
        if (time < 10.0f)
        {
            Debug.Log("no speed gained yet");
        }
    }
}
