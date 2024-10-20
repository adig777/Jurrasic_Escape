using UnityEngine;
using System.Collections;


public class Alien : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject healPrefab;
    private GameObject bullet;
    private float fireRate;
    private float nextFireTime;
    private float moveSpeed = 1f;
    private float amplitude = 3f;
    private float maxHeight = 9.3f;
    private float minHeight = -4f;
    private float startY;
    private SpriteRenderer sprite;
    private int hp = 4;
    private bool dead = false;
    private EnemySpawn spawns;

    //private int signal = 0;

    private Transform playerTransform;

    private void Start()
    {
        spawns = GameObject.FindObjectOfType<EnemySpawn>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nextFireTime = Time.time + fireRate;
        startY = transform.position.y;
        sprite = GetComponent<SpriteRenderer>();
        dead = false;
        hp = 4;
    }

    private void OnEnable()
    {
        hp = 4;
        dead = false;
    }

    private void Update()
    {
            
            float newY = startY + amplitude * Mathf.Sin(Time.time * moveSpeed);
            newY = Mathf.Clamp(newY, minHeight, maxHeight);
            Vector3 direction = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            fireRate = Random.Range(0.75f, 1.15f);
            
            if(hp == 0){
            dead = true;
            }



            if (Time.time > nextFireTime)
            {
                ShootBullet();
                nextFireTime = Time.time + fireRate;
            }
      
        
        
    }

    private void ShootBullet()
    {
        Vector3 pos = transform.position + Vector3.left*1.5f;
        if (!dead)
        {
            bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
        }
        else
        {
            bullet = Instantiate(healPrefab, pos, Quaternion.identity);
        }
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = direction * (2f * GameManager.Instance.gameSpeed);

    }


    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ChangeColorToRed());
        hp--;
        Destroy(other.gameObject);
    }


    IEnumerator ChangeColorToRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;

    }

    //public void into_out_frame()
    //{
    //    if(transform.position.x > 9.36 && GameManager.Instance.getseq())
    //    {
    //        transform.position += Vector3.left * 2.88f;
    //        signal = 0;
    //    }else if (transform.position.x < 12.24 && GameManager.Instance.getseq())
    //    {
    //        transform.position += Vector3.right * 2.88f;
    //        signal = 0;
    //    }
    //    else
    //    {
    //        ;
    //    }


    //}
}
