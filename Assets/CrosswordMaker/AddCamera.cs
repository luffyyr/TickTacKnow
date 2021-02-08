using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCamera : MonoBehaviour
{
   

    private void Start()
    {     
        GetComponent<Canvas>().worldCamera = Camera.main;
        transform.position = GetComponentInParent<GameObject>().transform.position;
    }
}
