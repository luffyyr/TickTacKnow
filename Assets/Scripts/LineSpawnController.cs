using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawnController : MonoBehaviour
{
    public static LineSpawnController Instance;
    public Vector3 startPos;
    public Vector3 endPos;
    public GameObject line;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CreateLine();
        }
    }

    void CreateLine()
    {
        GameObject var = Instantiate(line, transform.position, Quaternion.identity) as GameObject;
        //LineDrawer xyz = var.GetComponent<LineDrawer>();
        //xyz.LineFun(startPos,endPos);
    }

}
