using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : MonoBehaviour
{
    public GameObject d1, d2, d3, Boss;
    private SpriteRenderer sprite;
    private bool running = true;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sprite.enabled == true && running)
        {
            d1.gameObject.SetActive(true);
            d2.gameObject.SetActive(true);
            d3.gameObject.SetActive(true);
            StartCoroutine(BossStart());
        }
    }

    IEnumerator BossStart()
    {
        running = false;
        yield return new WaitForSeconds(0.5f);
        Boss.gameObject.SetActive(true);
        enabled = false;
    }
}
