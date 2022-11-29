using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class LineObstacle : MonoBehaviour
{
    public Transform[] nodes;
    public LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider;

    private void Awake() {
        edgeCollider = lineRenderer.GetComponent<EdgeCollider2D>();
    }

    private void Start() {
        if(nodes.Length > 2 || nodes.Length < 2)
        {
            throw new Exception("Nodes array must contain 2 elements");
        }
    }

    private void Update(){
        SetLineRenderer();
        SetEdgeCollider();
    }

    private void SetLineRenderer(){
        lineRenderer.SetPositions(GetPoses().ToArray());
    }

    private void SetEdgeCollider(){
        edgeCollider.SetPoints(GetLocalPoses().ToList());
    }

    private IEnumerable<Vector3> GetPoses(){
        foreach(var element in nodes)
        {
            yield return element.position;
        }
    }

    private IEnumerable<Vector2> GetLocalPoses(){
        foreach(var element in nodes)
        {
            yield return element.localPosition;
        }
    }
}
