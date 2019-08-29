using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float speed = 3.0f;
    public float ChangeTime = 3.0f;
    public bool vertical;
    public ParticleSystem Smoke_Effect;

    Animator animator;
    Rigidbody2D rb2d;
    float timer;
    int direction = 1;
    bool broken;

    void OnCollisionEnter2D(Collision2D other)
    {
        Ruby_Controller player = other.gameObject.GetComponent<Ruby_Controller>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = ChangeTime;
        broken = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = ChangeTime;
        }

        Vector2 position = rb2d.position;

        if(vertical)
        {
            animator.SetFloat("Move_X", 0);
            animator.SetFloat("Move_Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("Move_X", direction);
            animator.SetFloat("Move_Y", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rb2d.MovePosition(position);
    }

    public void Fix()
    {
        broken = false;
        rb2d.simulated = false;
        animator.SetTrigger("Fixed");
        Smoke_Effect.Stop();
        Destroy(Smoke_Effect.gameObject);
    }
}
