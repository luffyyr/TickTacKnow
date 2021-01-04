using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomButton : MonoBehaviour
{
    Vector3 cachedScale;
    public float x;
    public float y;
    public float z;

    void Start()
    {

        cachedScale = transform.localScale;
    }

    public void OnPointerEnter()
    {

        transform.localScale = new Vector3(transform.localScale.x + x, transform.localScale.y + y, transform.localScale.z + z);
    }

    public void OnPointerExit()
    {

        transform.localScale = cachedScale;
    }
}
