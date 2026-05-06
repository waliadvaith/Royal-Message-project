using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

public class SpeedOvertime : MonoBehaviour
{
    // public varaibles to get componets
    public CharacterMovement CharacterMovementScript;
    public SpriteRenderer characterSR;
    public EnemyAI EnemyAIScript;
    public EnemyAI EnemyAI;
    private SpriteRenderer rb;
    // time componet
    public float time = 0.00f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // getting the componets of the characters movement
        characterSR = GetComponent<SpriteRenderer>();
        rb = GetComponent<SpriteRenderer>();
        CharacterMovementScript = characterSR.GetComponent<CharacterMovement>();
        EnemyAIScript = characterSR.GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        // the time scale
        time += Time.deltaTime;
        // this is supposed to cap the speed to not go too fast
        if (CharacterMovementScript.speed > 22.5f)
        {
            CharacterMovementScript.speed = 22.5f;
            EnemyAIScript.speed += 1.25f * Time.deltaTime;
                if(EnemyAIScript.speed > 23.0f)
                {
                    EnemyAIScript.speed = 23.0f;
                }
        }

        // happens if it has reached a current amount of time
        if (time >= 60.0f)
        {
             
            CharacterMovementScript.speed += 3 * Time.deltaTime;
            Debug.Log("Gotta Go fast. Gotta go fast");
        }
            if(time >= 30.0f)
            {
                CharacterMovementScript.speed += 2.5f * Time.deltaTime;
                Debug.Log("you been granted ultra speed");
            }
        
                if(time > 20.0f)
                {
                    CharacterMovementScript.speed += 2 * Time.deltaTime;
                    Debug.Log("you been granted even more speed");
                }
            if(time >= 10.0f)
            {
                    if(time <= 20.0f)
                    {
                        CharacterMovementScript.speed += 1.5f * Time.deltaTime;
                        Debug.Log("you been granted more speed");
                    }
                
            }
        if (time < 10.0f)
        {
            Debug.Log("no speed gained yet");
        }
    }
}
