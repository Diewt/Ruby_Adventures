using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health_Bar : MonoBehaviour
{
    public static UI_Health_Bar instance {get; private set;}
    public Image mask;
    
    float Original_Size;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Original_Size = mask.rectTransform.rect.width;
    }

    public void Set_Value(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Original_Size * value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
