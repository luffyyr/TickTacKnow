using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawnController : MonoBehaviour
{

    public Vector3 startPos;
    public Vector3 endPos;
    public GameObject line;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject var = Instantiate(line,transform.position,Quaternion.identity) as GameObject;
            //LineDrawer xyz = var.GetComponent<LineDrawer>();
            //xyz.LineFun(startPos,endPos);
        }
    }
}
