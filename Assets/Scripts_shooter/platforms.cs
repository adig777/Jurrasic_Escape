using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platforms : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer SSprite;
    // Start is called before the first frame update
    void Start()
    {
        SSprite = GetComponent<SpriteRenderer>();
        StartCoroutine(animate());
        
    }

    IEnumerator animate()
    {
        SSprite.sprite = sprites[1];
        yield return new WaitForSeconds(1f);
        SSprite.sprite = sprites[0];
        yield return new WaitForSeconds(1f);
        StartCoroutine(animate());

    }
}
