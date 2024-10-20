using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour
{
    private float leftedge;
    private float rightedge;
    public float gravity = 9.81f * 2f;
    private bool yeet = false;
    private SpriteRenderer spriterenderer;
    private Rigidbody rb;
    private Vector3 force;
    public Sprite[] sprites;
    private int frame = 1;


    private void Start()
    {
        leftedge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
        rightedge = Camera.main.ScreenToWorldPoint(Vector3.zero).x + 50f;
        transform.position += Vector3.left * Random.Range(-3.5f, 6f);
        rb = GetComponent<Rigidbody>();
        force = new Vector3(17f, 15f, 0f);
        spriterenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (transform.position.y > -1.8)
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;
        }
        else
        {
            if (!yeet)
            {
                if (frame == 1)
                {
                    spriterenderer.sprite = sprites[frame];
                    StartCoroutine(waitframe());
                }

                transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 9));
            }
        }

        if (transform.position.x < leftedge || transform.position.x > rightedge)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {

            if (other.CompareTag("forcefield"))
            {
                frame = 0;
                spriterenderer.sprite = sprites[frame];
                yeet = true;
                rb.AddForce(force, ForceMode.Impulse);
            }
        

    }


    IEnumerator waitframe()
    {
        yield return new WaitForSeconds(0.1f);
        frame = 2;
        spriterenderer.sprite = sprites[frame];

    }

}
