using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Ajuste la vitesse du défilement
    public float resetPosition = -20f; // Position à laquelle l'objet se réinitialise
    public float startPosition = 20f; // Position à laquelle l'objet commence

    private void Update()
    {
        // Déplace le décor vers la gauche
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // Si l'objet sort de l'écran, le réinitialiser à la position de départ
        if (transform.position.x <= resetPosition)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        // Réinitialise l'objet à la position de départ
        transform.position = new Vector3(startPosition, transform.position.y, transform.position.z);
    }
}
