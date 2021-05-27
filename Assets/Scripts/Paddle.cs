using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    // Modifieurs de bonus/malus
    public float speed = 5f; // Vitesse de la raquette
    public float scale = 2f; // Taille de la raquette
    public bool isInverted = false; // Contrôles inversés
    public bool isFastPaddle = false; // Raquette rapide

    public Material defaultSpeedMaterial;
    public Material defaultSpriteMaterial;
    public float rightScreenEdge;
    public float leftScreenEdge;
    public GameManager gameManager;
    public Ball ball;

    private float input;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (gameManager.gameOver) // Si game over : on a plus accès au reste du script.
        {
            return;
        }

        input = Input.GetAxisRaw("Horizontal"); // Z/S ou LEFT/RIGHT

        if (isInverted)
        {
            input = -input; // Inverser les contrôles
        }

        speed = isFastPaddle ? 10f : 5f; // Si bonus vitesse : raquette 2x plus rapide
        spriteRenderer.material = (isFastPaddle ? defaultSpeedMaterial : defaultSpriteMaterial); // Matériaux pour raquette rapide
        
        // Gestion des vecteurs et des bords de map pour diriger la raquette :
        transform.Translate(Vector2.right * input * Time.deltaTime * speed);
        if (transform.position.x < leftScreenEdge + 0.5f*(scale-2f))
        {
            transform.position = new Vector2(leftScreenEdge + 0.5f*(scale-2f), transform.position.y);
        }
        if (transform.position.x > rightScreenEdge - 0.5f*(scale-2f))
        {
            transform.position = new Vector2(rightScreenEdge - 0.5f*(scale-2f), transform.position.y);
        }

        transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
    }

        void OnTriggerEnter2D(Collider2D other) // Collision avec un autre objet
    {
        if (other.CompareTag("ExtraLife")) // BONUS Vie supplémentaire
        {
            gameManager.AddLive();
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject, 1f);
        }

        if (other.CompareTag("LargePaddle")) // BONUS Raquette large
        {
            UpdatePaddleSize(1);
            PowerupDisplay.NewLargePaddle();
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject, 1f);
        }

        if (other.CompareTag("SmallPaddle")) // MALUS Raquette petite
        {
            UpdatePaddleSize(-1);
            PowerupDisplay.NewSmallPaddle();
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject, 1f);
        }

        if (other.CompareTag("InvertedPaddle")) // MALUS Raquette inversée
        {
            UpdatePaddleInverted(true);
            PowerupDisplay.NewInvertedPaddle();
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject, 1f);
        }

        if (other.CompareTag("FastPaddle")) // BONUS Raquette rapide
        {
            UpdateFastPaddle(true);
            PowerupDisplay.NewFastPaddle();
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(other.gameObject, 1f);
        }
    }

    public void UpdatePaddleSize(int value) // -1 ou +1
    {
        if((scale >= 1 && scale < 5 && value > 0) || (scale > 1 && scale <= 5 && value < 0))
        {
            scale += value;
        }
    }

    public void UpdatePaddleInverted(bool value) // true : inversé
    {
        isInverted = value;
        spriteRenderer.color = (value ? Color.red : Color.white); // inversé : raquette rouge sinon raquette blanche
        
    }

    public void UpdateFastPaddle(bool value)
    {
        isFastPaddle = value;
        spriteRenderer.material = (value ? defaultSpeedMaterial : defaultSpriteMaterial); // Matériaux pour raquette rapide
    }
}
