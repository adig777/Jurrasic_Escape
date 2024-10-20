using UnityEngine;

public class Animator : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite death;
    private SpriteRenderer spriterenderer;
    private int frame;
    public float fps = 0.75f;

    private void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void Animate()
    {
        if (GameManager.Instance.getground())
        {
            frame++;
            if (frame >= sprites.Length)
            {
                frame = 0;
            }
            if (frame >= 0 && frame < sprites.Length)
            {
                spriterenderer.sprite = sprites[frame];
            }
        }

        Invoke(nameof(Animate), fps / GameManager.Instance.gameSpeed);
    }
}
