using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectMouseInput : MonoBehaviour
{

    //public UIManager GetUIManager;
    public FindTiles GetFindTiles;
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit;

             hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

           

            if (hit.collider.gameObject.tag == "Tiles")
            {
                print(hit.collider.gameObject.GetComponent<DetectedInput>().line_across);
                string Acrword = hit.collider.gameObject.GetComponent<DetectedInput>().line_across;
                string Dwword = hit.collider.gameObject.GetComponent<DetectedInput>().line_down;
                GetFindTiles.FindWord(Acrword,Dwword);
            }

           
           
        }
    }
}
