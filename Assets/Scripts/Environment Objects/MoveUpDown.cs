using UnityEngine;

public class MoveUpDown : MonoBehaviour
{
    public Vector3 startPosition; // Initial position
    public Vector3 endPosition;   // Target position
    public float moveSpeed = 1.0f; // Movement speed

    private bool movingUp = true;

    private void Start()
    {
        transform.position = startPosition;
        InvokeRepeating("ToggleMovementDirection", 0f, 10f); // Toggle movement direction every 10 seconds.
    }

    private void Update()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void ToggleMovementDirection()
    {
        movingUp = !movingUp;
    }
}
