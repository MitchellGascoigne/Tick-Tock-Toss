using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

public class TimerPassable : Item
{
    protected TimerPassableInfo bombPassable;

    [SerializeField] Transform raycastPoint; // Used as the position and direction for the raycast.

    protected override void Initialise ()
    {
        base.Initialise();

        bombPassable = itemInfo as TimerPassableInfo; // Gives us a reference to TimerPassableInfo for convenience. This means that itemInfo should be of type TimerPassableInfo.
    }

    public override void Use ()
    {
        base.Use(); // Not strictly necessary, as this calls 'Item.Use()', which has no code. Keep this line here just in case 'Item.Use()' ever has code within it.

        TryPassBomb();
    }

    //void TryPassBomb ()
    //{
    //    if (!Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit hit, bombPassable.range))
    //        return;

    //    // Try find a PlayerController from the hit object. If it doesn't exist, stop executing code
    //    PlayerController controller = hit.collider.gameObject.GetComponent<PlayerController>();
    //    if (controller == null)
    //        return;

    //    PassBomb(controller.GetPhotonView().Controller);
    //}
    public void TryPassBomb()
    {
        // If the bombPassable range is greater than 0, use raycasting.
        if (bombPassable.range > 0f)
        {
            if (!Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit hit, bombPassable.range))
                return;

            PlayerController controller = hit.collider.gameObject.GetComponent<PlayerController>();
            if (controller == null)
                return;

            PassBomb(controller.GetPhotonView().Controller);
        }
        // If the bombPassable range is 0, use collision-based approach.
        else
        {
            // This part will be handled by the PlayerCollision script.
        }
    }


    // Put in a request for the MasterClient to change the timer's target.
    void PassBomb (Player target)
    {
        TimerManager.Instance.RequestTargetChange(target);
    }
}
