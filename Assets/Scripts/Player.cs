using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public TextMeshProUGUI cooldownText;
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

        if (!canSpawn)
        {
            cooldownText.gameObject.SetActive(true);
        }
        else
        {
            cooldownText.gameObject.SetActive(false);
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
            Vector3 spawnPosition = transform.position + fireballOffset;
            spawnPosition.y = transform.position.y;

            Instantiate(obj.prefab, spawnPosition, obj.prefab.transform.rotation);
            
            canSpawn = false; // Disable spawning during cooldown
            StartCoroutine(SpawnCooldown()); // Start cooldown coroutine
            break; 
        }
    }

    private IEnumerator SpawnCooldown()
    {
        float remainingCooldown = spawnCooldown; // Track remaining cooldown time

        while (remainingCooldown > 0)
        {
            cooldownText.text = "Cooldown: " + Mathf.Ceil(remainingCooldown); // Update the text
            yield return new WaitForSeconds(1f); // Wait for 1 second
            remainingCooldown -= 1f; // Decrease the remaining cooldown
        }

        canSpawn = true; // Enable spawning after cooldown
        cooldownText.gameObject.SetActive(false); // Hide cooldown text
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle") || other.CompareTag("Tree"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
