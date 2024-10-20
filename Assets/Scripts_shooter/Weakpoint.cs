using System.Collections;
using UnityEngine;

public class Weakpoint : MonoBehaviour
{
    public Boss boss;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.green;
        boss = FindObjectOfType<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * 8f * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
         Destroy(other.gameObject);
         boss.HP--;
         boss.takedamage();
         StartCoroutine(damage());
         if(boss.HP == 0)
            {
                boss.death();
            }
        }
    }

    IEnumerator damage()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.green;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = Color.green;
    }
}
