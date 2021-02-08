using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputValue : MonoBehaviour
{
    public string Text;
    private int pressed;



    private void Update()
    {
        if(pressed > 1 || GameManager.instance.TextValue != Text)
        {
            transform.GetComponent<Button>().image.color = Color.white;
            pressed = 0;
        }
    }


    public void TextValue()
    {
       // print(Text);
        GameManager.instance.TextValue = Text;    
        transform.GetComponent<Button>().image.color = Color.red;
        pressed++;
    }



    //    // Get the component
    //    tMP_Input = gameObject.GetComponent<TMP_InputField>();
    //// To get the text
    //string inputText = TMP_InputField tMP_Input = GetComponent<TMP_InputField>();

}
