using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Shooter : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite death;
    private SpriteRenderer spriterenderer;
    private CharacterController character;
    private int frame;
    public float fps = 0.5f;
    public float animationDelay = 0.045f;
    private float timeSinceLastFrameChange = 0f;


    private void Awake()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        character = GetComponent<CharacterController>();

    }

    private void FixedUpdate()
    {
        if (character.isGrounded && character.velocity.magnitude > 0)
        {
            timeSinceLastFrameChange += Time.deltaTime;

            if (timeSinceLastFrameChange >= animationDelay)
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

                timeSinceLastFrameChange = 0f;
            }
        }
        else if(character.isGrounded && character.velocity.magnitude == 0)
        {
            spriterenderer.sprite = sprites[0];
        }
        else
        {
            spriterenderer.sprite = sprites[0];

        }
    }

}
