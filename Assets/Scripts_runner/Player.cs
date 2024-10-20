using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
 public static Player Instance { get; private set; }
 private CharacterController character;
 private SpriteRenderer sprite;
 private Vector3 direction;
 public GameObject forcefield;
 public float gravity = 9.81f * 2f;
 public float jump = 9f;
 private float edge;
 public float hp = 3f;
 public bool invincible = false;
 public float forcefieldCooldown = 3f;
 public float lastforcetime;
 public Image cooldownindicater;
 public TextMeshProUGUI hptext;



private void Awake(){
    cooldownindicater.color = Color.green;
    character = GetComponent<CharacterController>();
    sprite = GetComponent<SpriteRenderer>();   
    edge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    hptext.text = "HP: "+ hp.ToString();
 }

 private void OnEnable(){
    sprite.color = Color.white;
    direction = Vector3.zero;
    lastforcetime = 0f;
 }

 private void Update(){

  hptext.text = "HP: "+ hp.ToString();
  direction += Vector3.down * gravity * Time.deltaTime;
        if (GameManager.Instance.allowinput)
        {
            if (character.isGrounded)
            {

                direction = Vector3.down;

                if (Input.GetButton("Jump"))
                {
                    GameManager.Instance.unground();
                    direction += Vector3.up * jump;
                }

            }
            character.Move(direction * Time.deltaTime);

            if (Input.GetButtonDown("S"))
            {

                if (Time.time - lastforcetime >= forcefieldCooldown)
                {
                    field();
                    invincible = true;
                    lastforcetime = Time.time;
                    StartCoroutine(InvincibilityTimer());
                }

            }

            if (Time.time - lastforcetime < forcefieldCooldown)
            {
                cooldownindicater.color = Color.red;
            }
            else
            {
                cooldownindicater.color = Color.green;
            }

        }

        if (transform.position.x < edge)
        {
            GameManager.Instance.GameOver();
        }
        
 }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Lava lava))
        {
           
        }
        else
        {
            Destroy(other.gameObject, 0.15f);
        }


        if (!invincible)
        {
            
            if (other.CompareTag("obstacle"))
            {
                StartCoroutine(ChangeColorToRed());
                if (hp > 1f)
                {
                    hp--;
                }
                else
                {
                    hp = 0f;
                    hptext.text = "HP: " + hp.ToString();
                    GameManager.Instance.GameOver();
                }
            }
            else if (other.CompareTag("heal") && hp < 3f)
            {
                StartCoroutine(ChangeColorToGreen());
                if (hp == 0.5f || hp == 1.5f)
                {
                    hp++;

                }
                else if (hp == 2.5f)
                {
                    hp = hp + 0.5f;

                }
                else
                {
                    hp++;

                }
            }
            else if (other.CompareTag("enemyproj"))
            {
                StartCoroutine(ChangeColorToRed());
                if (hp > 0.5f)
                {
                    //StartCoroutine(stutter());
                    hp = hp - 0.5f;

                }
                else
                {
                    hp = 0f;
                    hptext.text = "HP: " + hp.ToString();
                    GameManager.Instance.GameOver();
                }
            }
        }
        else
        {
           
        }
          
    }

    public void resethp()
    {
        if (hp == 0f)
        {
            hp = 3f;
        }
    }

    private void field()
    {
        Vector3 pos = transform.position;
        pos += Vector3.right * 2f;
        GameObject force = Instantiate(forcefield, pos, Quaternion.identity);
        Destroy(force, 0.25f);
        
    }

    IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSeconds(0.25f);
        invincible = false;
    }

    IEnumerator ChangeColorToRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        sprite.color =  Color.white;
    }

    IEnumerator ChangeColorToGreen()
    {
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
    }






}
