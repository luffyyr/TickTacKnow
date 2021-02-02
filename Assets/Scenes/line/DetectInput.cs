using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInput : MonoBehaviour
{
    int vertexCount = 0;
    bool mouseDown = false;
    LineRenderer line;




    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }


    [System.Obsolete]
    private void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
            }

            if (mouseDown)
            {
                SpawnLine();
            }

            if (Input.GetMouseButtonUp(0))
            {
                //DestroyLine();
            }

        }
    }

    [System.Obsolete]
    void SpawnLine()
    {
        //generating line
        line.SetVertexCount(vertexCount + 1);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        line.SetPosition(vertexCount, mousePos);
        vertexCount++;

        //adding box collider to line
        BoxCollider2D box = gameObject.AddComponent<BoxCollider2D>();
        box.transform.position = line.transform.position;
        box.size = new Vector2(4f, 0.3f);
    }

    [System.Obsolete]
    void DestroyLine()
    {
        mouseDown = false;
        vertexCount = 0;
        line.SetVertexCount(0);

        //destroying boxcolliders from lines
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D box in colliders)
        {
            Destroy(box);
        }
    }
}
