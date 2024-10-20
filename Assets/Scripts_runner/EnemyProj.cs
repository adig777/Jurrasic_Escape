using UnityEngine;

public class EnemyProj : MonoBehaviour
{
    public float lifetime = 10f;
    private Rigidbody rb;
    private SpriteRenderer sp;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sp = GetComponent<SpriteRenderer>();
        Destroy(gameObject, lifetime);

    }

    void OnTriggerEnter(Collider other)
    {

            if (other.CompareTag("forcefield"))
            {
                rb.velocity *= -1.5f;
                sp.flipX = true;
            }
        
    }
}
