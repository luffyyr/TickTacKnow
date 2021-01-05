using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInterface : MonoBehaviour
{
    public GameObject Menu;
    public AudioSource audioSource;
    public AudioClip hoverClip;

    public void OnPointerEnter()
    {
        audioSource.PlayOneShot(hoverClip);
        Menu.SetActive(true);

    }

    public void OnPointerExit()
    {
        Menu.SetActive(false);
    }
}
