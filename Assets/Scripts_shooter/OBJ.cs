using System.Collections;
using UnityEngine;

public class OBJ : MonoBehaviour
{

    public float gravity = 5.81f * 2f;
    private SpriteRenderer sprite;
    private Color gold;



    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        gold = new Color32(255, 215, 0, 255);

        StartCoroutine(goldglow());
    }

    void Update()
    {
        if (transform.position.y > -4.7)
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;
        }
        
    }

    IEnumerator goldglow()
    {
        sprite.color = gold;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.25f);
        sprite.color = gold;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.25f);
        sprite.color = gold;
        yield return new WaitForSeconds(0.25f);
        sprite.color = Color.white;
        StartCoroutine(goldglow());

    }

}
