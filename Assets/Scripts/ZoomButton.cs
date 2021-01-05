using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomButton : MonoBehaviour
{
    Vector3 cachedScale;
    public float x;
    public float y;
    public float z;

    public AudioClip hoverClip;
    public AudioSource audioSource;

    public GameObject Name;
    void Start()
    {
        cachedScale = transform.localScale;
    }

    public void OnPointerEnter()
    {
        audioSource.PlayOneShot(hoverClip);
        if(Name!= null)
        {
            Name.SetActive(true);
            transform.localScale = new Vector3(transform.localScale.x + x, transform.localScale.y + y, transform.localScale.z + z);
        }
            //Name.SetActive(true);
            transform.localScale = new Vector3(transform.localScale.x + x, transform.localScale.y + y, transform.localScale.z + z);
    }

    public void OnPointerExit()
    {
        audioSource.PlayOneShot(hoverClip);
        if (Name != null)
        {
            Name.SetActive(false);
            transform.localScale = cachedScale;
        }
        //Name.SetActive(false);
        transform.localScale = cachedScale;

    }
}
