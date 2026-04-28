using UnityEngine;

public class CastleWinTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the screens script on the player's canvas
            EndGameScreens screens = other.GetComponentInChildren<EndGameScreens>();

            if (screens != null)
            {
                screens.ActivateWin();
            }
        }
    }
}