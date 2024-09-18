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
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    public float spawnCooldown = 2f; // Cooldown duration

    private bool canSpawn = true; // Tracks cooldown state

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

        // Detect left arrow key press and spawn object if cooldown allows
        if (Input.GetKeyDown(KeyCode.RightArrow) && canSpawn)
        {
            ShootFireball();
        }
    }

    private void ShootFireball()
    {
        foreach (SpawnableObject obj in objects)
        {      
            // Instantiate the prefab at the spawnPoint position and rotation
            Instantiate(obj.prefab);
            canSpawn = false; // Disable spawning during cooldown
            StartCoroutine(SpawnCooldown()); // Start cooldown coroutine
            break; // Only spawn one object per press
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
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
