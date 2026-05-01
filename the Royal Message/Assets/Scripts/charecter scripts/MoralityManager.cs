using UnityEngine;

public class MoralityManager : MonoBehaviour
{
    
    public static MoralityManager Instance;

    public int MoralityScore = 0;
    public int BountyLevel = 0;
    public int MaxMorality = 50;
    public int MinMorality = -50;
    public BountyHunterManager BountyHunt;

    private void Awake()
    {
        
        if (Instance == null) Instance = this;
    }

    // 3. Remove 'static' from the method
    public void AddMorality(int amount)
    {
        
        MoralityScore += amount;

        
        MoralityScore = Mathf.Clamp(MoralityScore, MinMorality, MaxMorality);


        if (BountyHunt != null)
        {
            
            if (MoralityScore < 10)
            {
                float evilFactor = Mathf.Abs(MoralityScore) / (float)Mathf.Abs(MinMorality);
                BountyHunt.currentBountyLevel = Mathf.RoundToInt(Mathf.Pow(evilFactor, 2) * 5);
            }
            else
            {
                
                BountyHunt.currentBountyLevel = 0;
            }
        }

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) AddMorality(-10);
        if (Input.GetKeyDown(KeyCode.E)) AddMorality(10);
    }

}
