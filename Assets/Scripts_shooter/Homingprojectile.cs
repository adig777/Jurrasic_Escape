
using UnityEngine;

public class Homingprojectile : MonoBehaviour
{

    private Player_shooter player;
    private Transform playertransform;
    private Vector3 direction;
    private Vector3 velocity;
    private float projectilespeed = 2.75f;


    void Start()
    {
        player = FindObjectOfType<Player_shooter>();
        playertransform = player.transform;
        Destroy(gameObject, 5f);
    }


    void Update()
    {
        direction = playertransform.position - transform.position;
        direction.Normalize();
        velocity = direction * projectilespeed;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("forcefield"))
        {
            Destroy(gameObject);
        }
    }
}
