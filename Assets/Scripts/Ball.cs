using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Variables liées au composants de la balle
    public Rigidbody2D rb;
    public bool inPlay;
    public Transform paddle;

    public float speed = 4f;
    private bool isLaunched = false;
    public Transform explosion;
    public Transform[] powerUps;

    private AudioSource audio;
    private SpriteRenderer spriteRenderer;
    public GameManager gameManager;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (gameManager.gameOver) // GameOver : on freine la balle
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if(!inPlay) // On perd une vie : remettre la balle sur la raquette
        {
            ResetBallPosition();
        }

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Vertical")) && !inPlay) // Lancer la balle avec Espace Ou Z ou UP
        {
            inPlay = true;
            rb.AddForce(Vector2.up * 100f * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("BottomBorder")) // On touche le bord inférieur: faire perdre une vie
        {
            rb.velocity = Vector2.zero;
            inPlay = false;
            gameManager.RemoveLive(); // Retirer une vie
            other.gameObject.GetComponent<AudioSource>().Play(); // Jouer le petit son 
        }  
    }
    
    public void ResetBallPosition() // Pour remettre la balle sur la raquette
    {
        rb.velocity = Vector2.zero; // Freiner = vitesse nulle
        transform.position = paddle.position;
    }

    void OnCollisionEnter2D(Collision2D other) // Collision avec un autre objet
    {
        audio.pitch = Random.Range(0.95f, 1.05f);
        audio.Play(); // On joue un petit bruit de collision avec un pitch + ou - 5% pour varier un peu
        if(other.transform.CompareTag("Brick")) // Si on rentre en collision avec une brique
        {
            Brick brick = other.gameObject.GetComponent<Brick>();
            if(brick.durability > 1) // Certaines briques sont plus solides : durabilité > 1
            {
                brick.BreakBrick();
            } else
            {

                int randChance = Random.Range(1, 101);
                int randItemIndex = Random.Range(0, powerUps.Length);
                if (randChance < 20) // 20% de chance d'avoir un bonus en cassant une brique
                {
                    Instantiate(powerUps[randItemIndex], other.transform.position, other.transform.rotation);  // on instantie un objet aléatoire
                }

                // ParticleSystem pour gérer l'explosion de la brique avec la bonne couleur //
                ParticleSystem ps = explosion.gameObject.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.startColor = new ParticleSystem.MinMaxGradient(
                    new Color(brick.color.r * 1.5f, brick.color.g * 1.5f, brick.color.b * 1.5f), // plus clair
                    new Color(brick.color.r / 1.5f, brick.color.g / 1.5f, brick.color.b / 1.5f)  // plus sombre
                ); // random between 2 colors pour le particlesystem

                // Instantier l'explosion
                Transform newExplosion = Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(newExplosion.gameObject, 2.5f);

                gameManager.AddScore(other.gameObject.GetComponent<Brick>().points); // Ajouter au score le nb de points de la brique
                gameManager.UpdateNumberOfBricks(); // Indiquer au GameManager qu'on a perdu une brique
                Destroy(other.gameObject); // Détruire la brique à la fin.
            }

            
        }

        
    }
}
