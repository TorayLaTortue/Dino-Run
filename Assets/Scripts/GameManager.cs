using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set;}

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
        else { 
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this) {
            instance = null;
        }
    }

    public void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        gameSpeed = initialGameSpeed;
    }
    private void Update()
    {
        gameSpeed += gameSpeedIncrease + Time.deltaTime;
    }

}
