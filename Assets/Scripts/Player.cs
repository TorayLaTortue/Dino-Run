using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;

    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
    }

    public SpawnableObject[] objects;
    public float spawnCooldown = 4f; // Cooldown duration
    public Vector3 fireballOffset = new Vector3(1f, 0f, 0f); // Offset to spawn fireball slightly in front of the player

    private bool canSpawn = true; 
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);

       
        if (Input.GetKeyDown(KeyCode.RightArrow) && canSpawn)
        {
            ShootFireball();
        }
    }

    private void ShootFireball()
{
    foreach (SpawnableObject obj in objects)
    {
        // Calculate the spawn position
        Vector3 spawnPosition = transform.position + fireballOffset;
        spawnPosition.y = transform.position.y; // Set y to player's y position

        // Instantiate the fireball at the calculated spawn position
        Instantiate(obj.prefab, spawnPosition, obj.prefab.transform.rotation);
        
        canSpawn = false; // Disable spawning during cooldown
        StartCoroutine(SpawnCooldown()); // Start cooldown coroutine
        break; 
    }
}


    // Coroutine to handle cooldown
    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldown); // Wait for cooldown duration
        canSpawn = true; // Enable spawning after cooldown
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Tree"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
