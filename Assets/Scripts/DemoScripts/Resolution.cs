using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resolution : MonoBehaviour
{
    public Text TextField = null;
    private int Width = 0;
    private int Height = 0;

    public void SetWidth(int width)
    {
        Width = width;
    }

    public void SetHeight(int height)
    {
        Height = height;
    }

    public void OnResize()
    {
        TextField.text = Width + "x" + Height;
        Screen.SetResolution(Width, Height, false);
    }
   
}
