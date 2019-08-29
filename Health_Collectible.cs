using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Collectible : MonoBehaviour
{
    public AudioClip Collected_Clip;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object that entered trigger: " + other);

        Ruby_Controller controller = other.GetComponent<Ruby_Controller>();

        if (controller != null)
        {
            if(controller.Health < controller.Max_Health)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);    

                controller.Play_Sound(Collected_Clip);            
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
