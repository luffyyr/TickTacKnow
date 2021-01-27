using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharObject : MonoBehaviour
{
    public char character;
    public Text text;
    public Image image;

    public RectTransform rectTransform;
    public int index;

    [Header("Appearance")]
    public Color normalColor;
    public Color selectedColor;

    private Vector3 originalScale;

    bool isSelected = false;
    public CharObject Init(char c)
    {
        character = c;
        text.text = c.ToString();
        gameObject.SetActive(true);
        return this;
    }
    
    public void Select()
    {
        isSelected = !isSelected;

        image.color = isSelected ? selectedColor : normalColor;

        if (!isSelected)
        {
            WordScramble.main.DeletethisChar(this);

        }
    }

    private void Awake()
    {
        originalScale = transform.localScale;
       transform.localScale = new Vector3(0f, 0f, 0f);   // setting the scale value to zero at start
    }
    private void Start()
    {
        transform.DOScale(new Vector3(.9f, .9f, .9f), 1f);  // we are animating this through dotween
        //transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).OnComplete(() => { transform.DOShakeScale(.2f,1f,10,0,false); });
        transform.DORotate(new Vector3(0f, 0f, 360f), 1f, RotateMode.FastBeyond360).OnComplete(() => { transform.DOPunchScale(new Vector3(.5f,.5f,.5f),.25f); });
        //rectTransform.DOJump(new Vector3(16,16,0),5f,1,1f);

    }
}
