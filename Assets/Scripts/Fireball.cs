using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float maxRange;
    private Player player;

    private void Start()
    {
        if (player != null)
        {
            maxRange = player.transform.position.x + 1.5f;
        }    }

    private void Update() 
    {
        transform.position -= Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        if (transform.position.x > maxRange) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            Destroy(other.gameObject); 
            Destroy(gameObject);     
        }
    }
}
