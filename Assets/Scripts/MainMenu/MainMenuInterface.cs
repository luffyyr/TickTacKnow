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
        audioSource.PlayOneShot(hoverClip,0.5F);
        Menu.SetActive(true);

    }

    public void OnPointerExit()
    {
        Menu.SetActive(false);
    }

    public void AlternateOnPointExit()
    {
        StartCoroutine(Dothis());
    }

    IEnumerator Dothis()
    {
        yield return new WaitForSeconds(2f);
        Menu.SetActive(false);
    }
}
