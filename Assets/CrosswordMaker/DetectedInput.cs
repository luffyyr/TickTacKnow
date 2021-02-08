using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//for checking if answer is right or wrong
public class DetectedInput : MonoBehaviour
{
    public string text;
    public string line_down;
    public string line_across;
    bool run;
   


    private void Awake()
    {
       
    }


    private void Start()
    {
        
        text = transform.GetComponentInChildren<OTTextSprite>().text.ToUpper();
    }

    private void Update()
    {
           
        if (transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().text == text)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().image.color = Color.green;
            transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().interactable = false;

            if (!run)
            {
                UIManager.instance.rightCount++;
                run = true;
            }

          

        }

        else if(transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().text != text && transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().text != "")
        {
            transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().image.color = Color.red;
        }

        transform.GetChild(1).GetChild(0).GetComponent<TMP_InputField>().onValidateInput += delegate (string input, int charIndex, char addedChar) { return SetToUpper(addedChar); };   

    }

    public char SetToUpper(char c)
    {
        string str = c.ToString().ToUpper();
        char[] chars = str.ToCharArray();
        return chars[0];
    }



}
