using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftedge;
    private float rightedge;
    private bool yeet = false;
    private Rigidbody rb;
    private Vector3 force;

    private void Start()
    {
        leftedge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        rightedge = Camera.main.ScreenToWorldPoint(Vector3.zero).x + 50f;
        rb = GetComponent<Rigidbody>();
        force = new Vector3(17f, 15f, 0f);
    }

    private void Update()
    {
        if (!yeet)
        {
            transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 9));
        }
       

        if(transform.position.x < leftedge || transform.position.x > rightedge)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {

            if (other.CompareTag("forcefield"))
            {
                yeet = true;
                rb.AddForce(force, ForceMode.Impulse);
            }
     }


}
