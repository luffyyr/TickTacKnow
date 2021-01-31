using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer lineRed;
    private Vector2 mousePos;
    private Vector2 startMousePos;

    private float distance;

    void Start()
    {
        lineRed = GetComponent<LineRenderer>();
        lineRed.positionCount = 2;
        transform.position = new Vector3(0, 0, 0);
        LineFun();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRed.SetPosition(0, new Vector3(startMousePos.x, startMousePos.y, 0f));
            lineRed.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 0f));
            distance = (mousePos - startMousePos).magnitude;
            Debug.Log(distance);
        }
    } 


    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.1f, 0.1f);
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }

    public void LineFun()
    {  
        lineRed.SetPosition(0,this.transform.position);
        lineRed.SetPosition(1, new Vector3(1,1,0));
    }
}
