using UnityEngine;

public class House : MonoBehaviour
{
    public GameObject[] itemsToSpawn;
    private bool isNearHouse = false;
    
    void Start()
    {

    }

    void Update()
    {
        // Check if player is near a house and presses the 'E' key
        if (isNearHouse && Input.GetKeyDown(KeyCode.E))
        {
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
            isNearHouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("house"))
        {
            isNearHouse = false;
        }
    }
   
}

