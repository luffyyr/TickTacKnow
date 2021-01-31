using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YsrLetter : MonoBehaviour
{
    public bool utilized = false;
    public bool identified = false;
    public TextMesh letter;
    public int gridX, gridY;
    public bool anim = false;

    void Start()
    {
        //GetComponent<Renderer>().materials[0].color = WordSearch.Instance.defaultTint;
        GetComponent<Renderer>().materials[0].color = Ysr.Instance.defaultTint;
        //Rotate();
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
                if(anim == false)
                {
                    anim = true;
                    var tran = transform.localScale;
                    //Debug.Log("scaling");
                    //transform.DOScale(new Vector3(transform.localScale.x + .5f, transform.localScale.y + .5f, transform.localScale.z + .5f), 1f);  // we are animating this through dotween
                    transform.DOPunchScale(new Vector3(transform.localScale.x - .2f , transform.localScale.y - .2f , transform.localScale.z - .2f), .5f, 10, 0f);//.OnComplete(() => { transform.DOScale(tran, .5f); });   // we are animating this through dotween
                    // we are animating this through dotween                   
                }              
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

    public void Rotate()
    {
        StartCoroutine(CoReset());
    }

    IEnumerator CoReset()
    {
        int choseNumber = 0;
        yield return new WaitForSeconds(1f);
        if (Random.value < 0.5f)
            choseNumber = 360;
        else
            choseNumber = -360;
        transform.DORotate(new Vector3(0f, 0f, choseNumber), 1f, RotateMode.FastBeyond360);
    }
}

