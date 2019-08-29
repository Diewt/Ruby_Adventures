using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void launch(Vector2 direction, float force)
    {
        rb2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Enemy_Controller enemy = other.collider.GetComponent<Enemy_Controller>();
        if(enemy != null)
        {
            enemy.Fix();
        }

        //sents message to terminal to confirm collision
        Debug.Log("Projectile collision with" + other.gameObject);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
}
