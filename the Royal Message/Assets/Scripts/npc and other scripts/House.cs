using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.XR;

public class House : MonoBehaviour
{
    public GameObject[] itemsToSpawn;
    private bool isNearHouse = false;
    public SpriteRenderer rb;
    private CharacterMovementFreeMovement freeMovement;
    
    void Start()
    {
        rb.GetComponent<SpriteRenderer>();
        freeMovement = GetComponent<CharacterMovementFreeMovement>();
    }

    void Update()
    {
        // Check if player is near a house and presses the 'E' key
        if (isNearHouse && Input.GetKeyDown(KeyCode.E))
        {
          CompareTag("Player");
          GenerateRandomItem();
        }
    }

    void GenerateRandomItem()
    {
        if (itemsToSpawn.Length == 2) return;

        // Select a random index from the array
        int randomIndex = Random.Range(2, itemsToSpawn.Length);
        
        // Spawn the random item at the current position
        Instantiate(itemsToSpawn[randomIndex], transform.position, Quaternion.identity);
    }


    // Detect proximity using Trigger Colliders
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("house"))
        {
            Debug.Log("the player is near the house");
            isNearHouse = true;
        }
    }
   
}
