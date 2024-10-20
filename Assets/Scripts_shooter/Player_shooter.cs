using System.Collections;
using TMPro;
using UnityEngine;



public class Player_shooter : MonoBehaviour
{
    private CharacterController character;
    private int hp = 6;
    private SpriteRenderer sprite;
    public GameObject bulletPrefab;
    public GameObject bossdoor;
    public GameObject door2;
    public GameObject door3;
    public GameObject forcefield;
    public float gravity = 9.81f * 2f;
    public float moveSpeed = 7f;
    private Vector3 velocity;
    private float jumpVelocity = 12f;
    private bool facingright = true;
    public float bulletSpeed = 27f;
    private bool invincible = false;
    public int numshots = 6;
    private float rechargerate = 1.5f;
    private float forcerechargerate = 4f;
    private bool empty;
    public bool ismoving;
    public bool haskey = false;
    private float jumpboost = 19f;
    private Camera cam;
    private float zoomSpeed = 0.5f;
    private float zoomDuration = 1.5f;
    private int capacity = 6;
    private bool oncooldown = false;
    public GameObject coil;
    private float offset = 1.082f;
    private SpriteRenderer coilsprite;
    public Sprite [] coilsprites;



    public TextMeshProUGUI hptext;
    public TextMeshProUGUI ammotext;
    public TextMeshProUGUI forcetext;





    private void Awake()
    {
        coilsprite = coil.GetComponent<SpriteRenderer>();
        character = GetComponent<CharacterController>();
        sprite = GetComponent<SpriteRenderer>();
        cam = GetComponentInChildren<Camera>();
    }

    private void OnEnable()
    {
        hp = 6;
        numshots = 6;
        hptext.text = "HP: " + hp;
        ammotext.text = "Shots: " + numshots;
        forcetext.text = "Forcefield: Ready";
        sprite.color = Color.white;
        haskey = false;
        door3.GetComponent<SpriteRenderer>().enabled = false;
        door3.GetComponent<BoxCollider>().isTrigger = true;
        door2.gameObject.SetActive(false);
        bossdoor.gameObject.SetActive(true);

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");


        Vector3 movementDirection = new Vector3(horizontalInput, 0f, 0f).normalized;


        
        velocity.y -= gravity * Time.deltaTime;

   
        if (Input.GetButton("Jump") && character.isGrounded)
        {
            velocity.y = jumpVelocity; 
        }

        Vector3 moveVelocity = movementDirection * moveSpeed + velocity;


        character.Move(moveVelocity * Time.deltaTime);

        if (horizontalInput < 0)
        {
           
            facingright = false;
            sprite.flipX = true;
            
        }
        else if (horizontalInput > 0)
        {
       
            facingright = true;
            sprite.flipX = false;
        }
        Vector3 coilPosition = transform.position;

        if (horizontalInput != 0f)
        {
            coilPosition += new Vector3((horizontalInput > 0f ? 0.5f : -0.5f), 0.85f, 0f) * offset;
        }
        else
        {
            float coilDirection = coil.transform.position.x - transform.position.x;
            coilPosition += new Vector3((coilDirection > 0f ? 0.5f : -0.5f), 0.85f, 0f) * offset;
        }
        coil.transform.position = coilPosition;


        if (character.isGrounded)
        {
            velocity.y = 0f;
        }

        if (Input.GetMouseButtonDown(0) && !empty)
        {
            shoot();
        }

        if (Input.GetKeyDown(KeyCode.E) && !oncooldown)
        {
           
            forcebubble();
            
        }

        
        


    }

    void forcebubble()
    {
        oncooldown = true;
        GameObject force = Instantiate(forcefield, transform.position, Quaternion.identity);
        StartCoroutine(rechargeforce());
        Destroy(force, 0.65f);
    }

    IEnumerator rechargeforce()
    {
        forcetext.text = "Forcefield: Recharging";
        yield return new WaitForSeconds(forcerechargerate);
        oncooldown = false;
        forcetext.text = "Forcefield: Ready";

    }

    IEnumerator coilspritechange()
    {
        coilsprite.sprite = coilsprites[1];
        yield return new WaitForSeconds(0.12f);
        coilsprite.sprite = coilsprites[0];
    }





    void shoot()
    {
        Vector3 pos = coil.transform.position + Vector3.up * 0.2f;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 shootDirection = worldMousePos - transform.position;
        shootDirection.z = 0f;
        shootDirection.Normalize();

        if (facingright)
        {
            pos = pos + Vector3.right * 0.19f;
            GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = shootDirection * bulletSpeed;
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            pos = pos + Vector3.left * 0.19f;
            GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = shootDirection * bulletSpeed;
            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x *= -1;
            bullet.transform.localScale = bulletScale;
            float angle = Mathf.Atan2(-shootDirection.y, -shootDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        StartCoroutine(coilspritechange());

        numshots--;
        ammotext.text = "Shots: " + numshots;
        if (numshots == 0)
        {
            empty = true;
            StartCoroutine(recharge());
        }
      
        
    }

    IEnumerator recharge()
    {
        for (int i = 0; i < capacity; i++)
        {
            yield return new WaitForSeconds(rechargerate / 3f);
            numshots++;
            ammotext.text = "Shots: " + numshots;
        }
        empty = false;
        
    }

    IEnumerator ChangeColorToRed()
    {
        invincible = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.white;
        invincible = false;
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("powerup"))
        {
            velocity.y = jumpboost;
        }

        if (other.CompareTag("enemyproj") && !invincible)
        {
            hp--;
            hptext.text = "HP: " + hp;
            Destroy(other.gameObject);
            if (hp == 0)
            {
                //transform.position = spawnpoint;
                ShooterGameManager.Instance.Gameover();
                
            }
            else
            {
                StartCoroutine(ChangeColorToRed());
            }

        }
        if(other.CompareTag("laserbeam") && !invincible)
        {
            hp = hp - 2;
            hptext.text = "HP: " + hp;
            if (hp <= 0)
            {
                //transform.position = spawnpoint;

                ShooterGameManager.Instance.Gameover();
            }
            else
            {
                StartCoroutine(ChangeColorToRed());
            }
        }

        if (other.CompareTag("key") && haskey == false)
        {
            Debug.Log("goekey");
            haskey = true;
           
            Destroy(other.gameObject);
        }

        if (other.CompareTag("door"))
        {
            if (haskey)
            {
                bossdoor.gameObject.SetActive(false);
                ShooterGameManager.Instance.bossState = true;
                haskey = false;
                StartCoroutine(closedoor());
                jumpboost = 24f;
                moveSpeed = 10f;
                StartCoroutine(ZoomOutCoroutine());
            }
            else
            {
                velocity.y = -5;
            }
        }

        if (other.CompareTag("obstacle")){
            StartCoroutine(ZoomInalittleCoroutine());
            ShooterGameManager.Instance.bossFight = true;
            hp = 6;
            haskey = false;
            hptext.text = "HP: " + hp;
            StartCoroutine(appeardoor());
            jumpVelocity = 18f;
            moveSpeed = 16f;
            bulletSpeed = 35f;
            
        }

    }

    IEnumerator appeardoor()
    {
        yield return new WaitForSeconds(0.35f);
        door3.GetComponent<SpriteRenderer>().enabled = true;
        door3.GetComponent<BoxCollider>().isTrigger = false;

    }



 

    IEnumerator ZoomOutCoroutine()
    {
        float originalSize = cam.orthographicSize;
        float zoomTargetSize = originalSize * 2f;
        float zoomTime = 0f;

        while (zoomTime < zoomDuration)
        {
            float newSize = Mathf.Lerp(originalSize, zoomTargetSize, zoomTime / zoomDuration);
            cam.orthographicSize = newSize;
            zoomTime += Time.deltaTime * zoomSpeed;
            yield return null;
        }

        cam.orthographicSize = zoomTargetSize;
    }

    IEnumerator ZoomInalittleCoroutine()
    {
        float originalSize = cam.orthographicSize;
        float zoomTargetSize = originalSize * 1.55f;
        float zoomTime = 0f;
        

        while (zoomTime < zoomDuration)
        {
            float newSize = Mathf.Lerp(originalSize, zoomTargetSize, zoomTime / zoomDuration);
            cam.orthographicSize = newSize;
            zoomTime += Time.deltaTime * zoomSpeed;
            yield return null;
        }

        cam.orthographicSize = zoomTargetSize;
       
    }

    IEnumerator closedoor()
    {
        yield return new WaitForSeconds(0.15f);
        door2.gameObject.SetActive(true);
    }
}
