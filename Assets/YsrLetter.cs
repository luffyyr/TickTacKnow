using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YsrLetter : MonoBehaviour
{
    public bool utilized = false;
    public bool identified = false;
    public TextMesh letter;
    public int gridX, gridY;

    void Start()
    {
        //GetComponent<Renderer>().materials[0].color = WordSearch.Instance.defaultTint;
        GetComponent<Renderer>().materials[0].color = Ysr.Instance.defaultTint;
    }

    void Update()
    {

        if (Ysr.Instance.ready)
        {
            if (!utilized && Ysr.Instance.current == gameObject) //checking if the current selected object in WordSearch is this object
            {
                Ysr.Instance.selected.Add(this.gameObject);
                GetComponent<Renderer>().materials[0].color = Ysr.Instance.mouseoverTint;     //changing color since we have selected this object
                Ysr.Instance.selectedString += letter.text; // passing the char this object stored in the selectedString
                utilized = true;                  //ultizing is true since we are using it now
            }
        }

        if (identified)
        {
            if (GetComponent<Renderer>().materials[0].color != Ysr.Instance.identifiedTint)
            {
                GetComponent<Renderer>().materials[0].color = Ysr.Instance.identifiedTint;
            }
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            utilized = false;
            if (GetComponent<Renderer>().materials[0].color != Ysr.Instance.defaultTint)
            {
                GetComponent<Renderer>().materials[0].color = Ysr.Instance.defaultTint;
            }
        }
    }
}

