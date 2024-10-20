using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int HP = 24;
    public GameObject[] weakpoints;
    public GameObject[] lasers;
    public GameObject[] pointers;
    public Sprite[] sprites;
    private float laserrate = 7f;
    private float lastlasertime;
    private int laser1, laser2, oldlaser1, oldlaser2, weakpointpos;
    private SpriteRenderer sprite;
    private bool dying;
    private bool falling = true;
    



    void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(fallingg());
    }

    IEnumerator fallingg()
    {
        while (transform.position.y > 174f)
        {
            transform.position += Vector3.down * 20.1f * Time.deltaTime;
        }
        falling = false;
        lastlasertime = Time.time - 5f;
        yield return null;
    }


    void Update()
    {
            
        //if(transform.position.y > 174f)
        //{
        //    transform.position += Vector3.down * 35.1f * Time.deltaTime;
        //}
        //else
        //{
        //    falling = false;
        //}
        
        
        laser1 = Random.Range(0, 3);
        laser2 = Random.Range(0, 3);
        weakpointpos = 3 - laser1 - laser2;
        if (Time.time - lastlasertime > laserrate && laser1 != laser2 && laser1 != oldlaser1 && laser2 != oldlaser2 && !dying && !falling)
        {
            oldlaser1 = laser1;
            oldlaser2 = laser2;
            lastlasertime = Time.time;
            StartCoroutine(firelaser(laser1, laser2, weakpointpos));
        }
        


    }

    IEnumerator firelaser(int a, int b, int c)
    {
        pointers[a].gameObject.SetActive(true);
        pointers[b].gameObject.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        sprite.sprite = sprites[1];
        pointers[a].gameObject.SetActive(false);
        pointers[b].gameObject.SetActive(false);
        weakpoints[c].gameObject.SetActive(true);
        lasers[a].gameObject.SetActive(true);
        lasers[b].gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        sprite.sprite = sprites[0];
        weakpoints[c].gameObject.SetActive(false);
        lasers[a].gameObject.SetActive(false);
        lasers[b].gameObject.SetActive(false);

    }

    public void takedamage()
    {
        StartCoroutine(damage());
    }

    IEnumerator damage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
    }

    IEnumerator die()
    {
        dying = true;
        sprite.sprite = sprites[1];
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.white;
        Destroy(gameObject);
        ShooterGameManager.Instance.win();
    }



    public void death()
    {
        foreach(var laser in lasers)
        {
            laser.gameObject.SetActive(false);
        }
        foreach (var points in weakpoints)
        {
            points.gameObject.SetActive(false);
        }
        StartCoroutine(die());
    }

}
