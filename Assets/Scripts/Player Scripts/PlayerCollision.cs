using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool hasTimer = false; // Indicates if the player has the timer
    public GameObject timerPrefab; // Reference to the timer GameObject

    private void OnCollisionEnter(Collision other)
    {
        if (hasTimer && other.gameObject.CompareTag("Player"))
        {
            PlayerCollision otherPlayerCollision = other.gameObject.GetComponent<PlayerCollision>();
            if (otherPlayerCollision != null)
            {
                // Transfer the timer to the collided player
                otherPlayerCollision.GainTimer();
                LoseTimer();
            }
        }
    }

    public void GainTimer()
    {
        if (timerPrefab != null)
        {
            // Instantiate and attach the timer prefab
            GameObject timerObject = Instantiate(timerPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
            timerObject.transform.SetParent(transform); // Attach the timer as a child
            hasTimer = true;
        }
        else
        {
            Debug.LogError("timerPrefab is not assigned.");
        }
    }



    public void LoseTimer()
    {
        if (timerPrefab != null)
        {
            // Destroy the timer GameObject
            Destroy(timerPrefab);
        }
        hasTimer = false;
    }
}
