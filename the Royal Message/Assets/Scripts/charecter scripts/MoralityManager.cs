using UnityEngine;

public class MoralityManager : MonoBehaviour
{

    public static int MoralityScore = 0;
    public static int BountyLevel = 0;

    public static void AddMorality(int amount)
    {
        MoralityScore += amount;


        if (MoralityScore < -10)
        {
            BountyLevel = Mathf.Abs(MoralityScore) / 5;
        }

        Debug.Log($"Global Morality Updated! Current Score: {MoralityScore} | Bounty: {BountyLevel}");
    }
}