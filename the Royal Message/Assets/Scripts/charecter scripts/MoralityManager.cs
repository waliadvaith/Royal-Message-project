using UnityEngine;

public class MoralityManager : MonoBehaviour
{
    
    public static MoralityManager Instance;

    public int MoralityScore = 0;
    public int BountyLevel = 0;
    public int MaxMorality = 50;
    public int MinMorality = -50;
    public BountyHunterManager BountyHunt;
    public delegate void MoralityChanged();
    public static event MoralityChanged OnMoralityChanged;
    private void Awake()
    {
        
        if (Instance == null) Instance = this;
    }

    // 3. Remove 'static' from the method
    public void AddMorality(int amount)
    {

        MoralityScore += amount;
        // This "shouts" to all Traders that the score changed
        OnMoralityChanged?.Invoke();

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




}
