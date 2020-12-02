using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    public float hp = 100f;
    private SpriteRenderer enemyRend;
    private Rigidbody2D enemyRb;
    // Start is called before the first frame update
    void Start()
    {
        enemyRend = GetComponent<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
