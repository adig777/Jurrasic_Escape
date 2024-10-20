using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp = 3f;
    private float movespeed = 2.85f;
    public GameObject energyball;
    public GameObject homingenergyball;
    public GameObject key;
    private SpriteRenderer spriteRenderer;
    private Player_shooter player;
    private Vector3 direction;
    private Vector3 velocity;
    private Transform playertransform;
    private float distanceToPlayer;
    private EnemySpawn spawns;
    private float lastlaser;
    private float laserate ;
    private int shootcounter = 0;
    private int hominginterval;



    void Start()
    {
        spawns = FindObjectOfType<EnemySpawn>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Player_shooter>();
        playertransform = player.transform;
        lastlaser = Time.time;
        hominginterval = Random.Range(4, 7);
        
    }

    void Update()
    {
        
        direction = playertransform.position - transform.position;
        distanceToPlayer = Vector3.Distance(playertransform.position, transform.position);
        
        if (distanceToPlayer <= 3f)
        {
            if (distanceToPlayer == 3f)
            {
                velocity = Vector3.zero;
            }
            else
            {
                direction.Normalize();
                velocity = -1f * movespeed * direction;
            }
        }
        else
        {

            direction.Normalize();
            velocity = direction * movespeed;
        }
        transform.position += velocity * Time.deltaTime;

        laserate = Random.Range(3.5f, 4.5f);

        if (Time.time - lastlaser > laserate && distanceToPlayer <= 10f)
        {
            lastlaser = Time.time;
            shoot();
        }

    }

    void shoot()
    {
        if (shootcounter < hominginterval)
        {
            shootcounter++;
            Vector3 direction = (playertransform.position - transform.position).normalized;
            GameObject energy = Instantiate(energyball, transform.position, Quaternion.identity);
            Rigidbody rb = energy.GetComponent<Rigidbody>();
            rb.velocity = direction * 6f;
        }
        else
        {
            shootcounter = 0;
            Instantiate(homingenergyball, transform.position, Quaternion.identity);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            StartCoroutine(ChangeColorToRed());
            hp--;
            Destroy(other.gameObject);
            if(hp == 0)
            {
                spawns.NumberEnemies--;
                spawns.totalenemies++;
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("enemyproj") && other.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
        {
            StartCoroutine(ChangeColorToRed());
            hp = hp - 2;
            if (hp <= 0)
            {
                spawns.NumberEnemies--;
                spawns.totalenemies++;
                Destroy(gameObject);

            }
        }
    }

    public void deactivate()
    {
        enabled = false;
    }


    IEnumerator ChangeColorToRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

}
