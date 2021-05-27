using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float speed;
    
    // Simple script pour faire chuter les objets sur l'écran
    void Update()
    {
        transform.Translate(new Vector3(0f, -1) * Time.deltaTime * speed);   

        if(transform.position.y < -6f)
        {
            Destroy(gameObject); // Si objet pas ramassé = trop bas, il est détruit
        }
    }
}
