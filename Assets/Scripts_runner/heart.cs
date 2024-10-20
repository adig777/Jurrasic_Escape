using UnityEngine;

public class heart : MonoBehaviour
{
    private float edge;

    private void Start()
    {
        edge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    private void Update()
    {
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        if (transform.position.x < edge)
        {
            Destroy(gameObject);
        }
    }
}
