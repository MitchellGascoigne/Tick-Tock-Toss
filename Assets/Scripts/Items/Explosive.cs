using UnityEngine;

public class Explosive : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;

    public GameObject explosionEffect;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            // Player Damage mucking with the photon view - fix!!

            //// Check if the nearby object is a player
            //PlayerController player = nearbyObject.GetComponent<PlayerController>();
            //if (player != null)
            //{
            //    // Destroy the player object
            //    Destroy(player.gameObject);
            //    // You might want to add more logic here, like decrementing player lives, showing game over screen, etc.
            //}
        }

        // Destroy the explosive object
        Destroy(gameObject);
    }
}
