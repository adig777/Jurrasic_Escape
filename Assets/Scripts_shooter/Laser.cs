
using UnityEngine;

public class Laser : MonoBehaviour
{
    private new Rigidbody rigidbody;
    private SpriteRenderer sprite;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("forcefield"))
        {
            rigidbody.velocity *= -1f;
            sprite.color = Color.red;
        }
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }



}
