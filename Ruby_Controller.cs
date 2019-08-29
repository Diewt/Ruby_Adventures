using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruby_Controller : MonoBehaviour
{
    public float speed = 3.0f;
    public float Time_Invincible = 2.0f;
    public int Max_Health = 5;
    public int Health{get {return Current_Health;}}
    public GameObject projectilePrefab;
    public AudioClip Throw_Sound;

    int Current_Health;
    bool Is_Invincible;
    float Invincible_Timer;
    Rigidbody2D rb2d;
    Animator animator;
    Vector2 Look_Direction = new Vector2(1,0);
    AudioSource Audio_Source;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Current_Health = Max_Health;
        Audio_Source = GetComponent<AudioSource>();

        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        //The above two lines of code set the frame rate of the game to 10 fps
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        //Debug.Log(horizontal); line used to display the values of horizontal to terminal
        //Debug.Log(verticacl); line used to display the values of vertical to terminal

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            Look_Direction.Set(move.x, move.y);
            Look_Direction.Normalize();
        }

        animator.SetFloat("Look X", Look_Direction.x);
        animator.SetFloat("Look Y", Look_Direction.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rb2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rb2d.MovePosition(position);

        if(Is_Invincible)
        {
            Invincible_Timer -= Time.deltaTime;
            if(Invincible_Timer < 0)
            {
                Is_Invincible = false;
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            launch();
        }

        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position + Vector2.up * 0.2f, Look_Direction, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                Debug.Log("Raycast has hit the object" + hit.collider.gameObject);
                NPC character = hit.collider.GetComponent<NPC>();
                if(character != null)
                {
                    character.Display_Dialog();
                }
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            if(Is_Invincible)
            {
                return;
            }
            
            Is_Invincible = true;
            Invincible_Timer = Time_Invincible;
        }

        Current_Health = Mathf.Clamp(Current_Health + amount, 0, Max_Health);
        Debug.Log(Current_Health + "/" + Max_Health);
        UI_Health_Bar.instance.Set_Value(Current_Health / (float)Max_Health);
    }

    public void Play_Sound(AudioClip clip)
    {
        Audio_Source.PlayOneShot(clip);
    }

    void launch()
    {
        GameObject Projectile_Object = Instantiate(projectilePrefab, rb2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = Projectile_Object.GetComponent<Projectile>();
        projectile.launch(Look_Direction, 300);

        animator.SetTrigger("Launch");

        Audio_Source.PlayOneShot(Throw_Sound);
    }
}
