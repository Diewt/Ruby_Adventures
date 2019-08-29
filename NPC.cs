using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float Display_Time = 4;
    public GameObject Dialog_Box;

    float Timer_Display;

    // Start is called before the first frame update
    void Start()
    {
        Dialog_Box.SetActive(false);

        Timer_Display = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Timer_Display >= 0)
        {
            Timer_Display -= Time.deltaTime;
            if(Timer_Display < 0)
            {
                Dialog_Box.SetActive(false);
            }
        }
    }

    public void Display_Dialog()
    {
        Timer_Display =Display_Time;
        Dialog_Box.SetActive(true);
    }
}
