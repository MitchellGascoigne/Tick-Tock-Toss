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

        foreach (Collider NearbyObject in colliders)
        {
            Rigidbody rb = NearbyObject.GetComponent<Rigidbody>();
            if (rb != null )
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        //Destroy(gameObject); we do not need to destroy anymore as this will be called from the inventory class
    }
}
