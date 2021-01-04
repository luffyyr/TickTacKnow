using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInterface : MonoBehaviour
{
    public GameObject Menu;

    void Start()
    {

    }

    public void OnPointerEnter()
    {
        Menu.SetActive(true);

    }

    public void OnPointerExit()
    {
        Menu.SetActive(false);
    }
}
