using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Handle collision with other players.
    private void OnCollisionEnter(Collision collision)
    {
        if (!TimerManager.IsLocalPlayerTarget()) // if we aren't the bomb target, don't try to pass the bomb.
            return;

        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            TimerManager.Instance.RequestTargetChange(controller.GetPhotonView().Controller);
        }
    }
}
