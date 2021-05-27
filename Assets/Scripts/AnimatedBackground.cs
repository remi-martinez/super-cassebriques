using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBackground : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer currentSprite;
    public float gifSpeed = 20f;
    
    void Start()
    {
        currentSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        currentSprite.sprite = sprites[(int)(Time.time * gifSpeed) % sprites.Length];
    }
}