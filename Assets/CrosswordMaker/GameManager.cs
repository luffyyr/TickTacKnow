using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string TextValue;

    private void Awake()
    {
        if(instance != this)
        instance = this;
    }
}
