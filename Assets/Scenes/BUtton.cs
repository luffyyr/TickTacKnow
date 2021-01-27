using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BUtton : MonoBehaviour
{
    public bool clicked = false;
    private SpriteState me;
    public Sprite pressssed;
    public Sprite notpressed;
    public Image lol;

    private void Start()
    {
       // me = GetComponent<Button>().spriteState;
        lol = GetComponent<Image>();
    }
    public void Press()
    {
        //me = new SpriteState();
        //me = lol.spriteState;
        if (clicked)
        {
            //me.pressedSprite = pressssed;
            lol.sprite = pressssed;
        }
        else
        {
            //me.pressedSprite = notpressed;
            lol.sprite = notpressed;
        }
        //lol.spriteState = me;
        clicked = !clicked;
        Debug.Log(clicked);
    }

}
