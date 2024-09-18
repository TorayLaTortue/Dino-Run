using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private GameObject tree;
    private float rightEdge;

    private void Start()
    {
        rightEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
    
    private void Update() 
    {
        transform.position -= Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;
        tree = GameObject.Find("tree(Clone)");
        if(transform.position.x > tree.transform.position.x) 
        {
            Destroy(tree);
        }

        if (transform.position.x > rightEdge) 
        {
            Destroy(gameObject);
        }

    }
}