using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class spawnonlywhendefeated : MonoBehaviour
{
    public GameObject portal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            // This will tell you the name of the first thing it finds with the tag
            Debug.Log("Still waiting on: " + enemies[0].name);
        }
        else
        {
            Debug.Log("All clear! Portal should be open.");
            portal.SetActive(true);
        }
    }

    void SpawnPortal()
    {
        // Your logic to enable or instantiate the portal here
        portal.SetActive(true);
    }
}
