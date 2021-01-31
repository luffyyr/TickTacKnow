using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detectline : MonoBehaviour
{
    public string aplhabetID;



    private void Awake()
    {
        
    }

    private void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Line")
        {
            // Destroy(gameObject);
            gameObject.GetComponent<Image>().color = Color.red;

            print(aplhabetID);
           
        }
    }


   
}
