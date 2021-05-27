using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 100; // Combien vaut la brique
    public int durability;
    public Color color;
    public Sprite hitSprite;

    public void BreakBrick()
    {
        durability--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }
}
