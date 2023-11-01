using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    protected TimerPassable timerPassable;

    private void Start()
    {
        timerPassable = GetComponent<TimerPassable>();
    }

    // Handle collision with other players.
    private void OnCollisionEnter(Collision collision)
    {
        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
           // timerPassable.TryPassBomb(controller.GetPhotonView().Controller);
        }
    }
}
