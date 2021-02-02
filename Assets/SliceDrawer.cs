using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SliceDrawer : MonoBehaviour
{
    public static SliceDrawer Instance;

    private new Camera camera;

    public Material lineMaterial;
    public float lineWidth;
    public float depth = 5;

    private Vector3? lineStartPoint = null;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        camera = GetComponent<Camera>();
    }

   /* void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineStartPoint = GetMouseCameraPoint();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(!lineStartPoint.HasValue)
            {
                return;
            }
            var lineEndPoint = GetMouseCameraPoint();
            var gameObject = new GameObject();
            var LineRenderer = gameObject.AddComponent<LineRenderer>();
            LineRenderer.material = lineMaterial;
            LineRenderer.SetPositions(new Vector3[] {lineStartPoint.Value ,lineEndPoint.Value });
            LineRenderer.startWidth = lineWidth;
            LineRenderer.endWidth = lineWidth;
            lineStartPoint = null;
        }
    }*/

    private Vector3? GetMouseCameraPoint()
    {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * depth;
    }

    public void CreateLine(Vector3 start, Vector3 end)
    {
        var gameObject = new GameObject();
        var LineRenderer = gameObject.AddComponent<LineRenderer>();
        LineRenderer.material = lineMaterial;
        LineRenderer.SetPositions(new Vector3[] { start, end });
        LineRenderer.startWidth = lineWidth;
        LineRenderer.endWidth = lineWidth;
        //lineStartPoint = null;
    }
}
